﻿using Aiursoft.DBTools;
using Aiursoft.Gateway.Data;
using Aiursoft.Scanner.Interfaces;
using Aiursoft.XelNaga.Tools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aiursoft.Gateway.Services
{
    public class TimedCleaner : IHostedService, IDisposable, ISingletonDependency
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IWebHostEnvironment _env;

        public TimedCleaner(
            ILogger<TimedCleaner> logger,
            IServiceScopeFactory scopeFactory,
            IWebHostEnvironment env)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_env.IsDevelopment() || !EntryExtends.IsProgramEntry())
            {
                _logger.LogInformation("Skip cleaner in development environment.");
                return Task.CompletedTask;
            }
            _logger.LogInformation("Timed Background Service is starting.");
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(10));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                _logger.LogInformation("Cleaner task started!");
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<GatewayDbContext>();
                await AllClean(dbContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
            }
        }

        public async Task AllClean(GatewayDbContext dbContext)
        {
            try
            {
                await ClearTimeOutOAuthPack(dbContext);
                _logger.LogInformation("Clean finished!");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
            }
        }

        public Task ClearTimeOutOAuthPack(GatewayDbContext dbContext)
        {
            var outDateTime = DateTime.UtcNow - TimeSpan.FromDays(1);
            var outDateTime2 = DateTime.UtcNow - TimeSpan.FromMinutes(20);
            dbContext.OAuthPack.Delete(t => t.UseTime < outDateTime);
            dbContext.OAuthPack.Delete(t => t.CreateTime < outDateTime2);
            return dbContext.SaveChangesAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
