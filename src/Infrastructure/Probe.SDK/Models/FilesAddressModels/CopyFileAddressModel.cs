﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Aiursoft.Probe.SDK.Models.FilesAddressModels
{
    public class CopyFileAddressModel
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        [FromRoute]
        public string SiteName { get; set; }
        [Required]
        [FromRoute]
        public string FolderNames { get; set; }
        [Required]
        public string TargetSiteName { get; set; }
        [Required]
        public string TargetFolderNames { get; set; }
    }
}
