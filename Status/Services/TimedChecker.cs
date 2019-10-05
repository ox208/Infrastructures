﻿namespace Aiursoft.Status.Services
{
    using global::Aiursoft.Pylon;
    using global::Aiursoft.Pylon.Models;
    using global::Aiursoft.Pylon.Services;
    using global::Aiursoft.Status.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    namespace Aiursoft.Probe.Services
    {
        public class TimedChecker : IHostedService, IDisposable
        {
            private Timer _timer;
            private readonly ILogger _logger;
            private readonly IServiceScopeFactory _scopeFactory;

            public TimedChecker(
                ILogger<TimedChecker> logger,
                IServiceScopeFactory scopeFactory)
            {
                _logger = logger;
                _scopeFactory = scopeFactory;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _logger.LogInformation("Timed Background Service is starting.");
                _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(10));
                return Task.CompletedTask;
            }

            private async void DoWork(object state)
            {
                try
                {
                    _logger.LogInformation("Cleaner task started!");
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<StatusDbContext>();
                        var http = scope.ServiceProvider.GetRequiredService<HTTPService>();
                        await AllCheck(dbContext, http);
                    }
                    _logger.LogInformation("Cleaner task finished!");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Cleaner crashed!");
                }
            }

            private async Task AllCheck(StatusDbContext dbContext, HTTPService http)
            {
                var items = await dbContext.MonitorRules.ToListAsync();
                await items.ForEachParallal(async t =>
                {
                    var content = await http.Get(new AiurUrl(t.CheckAddress), false);
                    var success = content.Contains(t.ExpectedContent);
                    t.LastHealthStatus = success;
                    dbContext.Update(t);
                });
                await dbContext.SaveChangesAsync();
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

}
