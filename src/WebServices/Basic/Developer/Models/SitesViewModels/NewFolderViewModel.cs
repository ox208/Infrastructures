﻿using Aiursoft.Developer.Models.AppsViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Aiursoft.Developer.Models.SitesViewModels
{
    public class NewFolderViewModel : AppLayoutModel
    {
        [Obsolete(message: "This method is only for framework", error: true)]
        public NewFolderViewModel() { }
        public NewFolderViewModel(DeveloperUser user) : base(user) { }
        public void Recover(DeveloperUser user, string appName)
        {
            RootRecover(user);
            AppName = appName;
        }

        public string NewFolderName { get; set; }

        [FromRoute]
        [Required]
        public string AppId { get; set; }
        [FromRoute]
        public string SiteName { get; set; }
        [FromRoute]
        public string Path { get; set; }
        public string AppName { get; set; }
    }
}
