﻿using Aiursoft.SDKTools.Attributes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aiursoft.Wrapgate.SDK.Models
{
#warning IP is not supported!
    public enum RecordType
    {
        /// <summary>
        /// Supports HTTP, HTTPS, IP.
        /// </summary>
        Redirect,
        /// <summary>
        /// Supports HTTP, HTTPS, IP
        /// </summary>
        PermanentRedirect,
        /// <summary>
        /// Supports HTTPS
        /// </summary>
        IFrame,
        /// <summary>
        /// Supports HTTP, HTTPS, IP
        /// </summary>
        ReverseProxy
    }

    [Index(nameof(RecordUniqueName), IsUnique = true)]
    public class WrapRecord
    {
        [Key]
        public int Id { get; set; }
        public string AppId { get; set; }
        [ForeignKey(nameof(AppId))]
        [JsonIgnore]
        public WrapgateApp App { get; set; }

        [ValidDomainName]
        public string RecordUniqueName { get; set; }
        [Url]
        public string TargetUrl { get; set; }
        public RecordType Type { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        public bool Enabled { get; set; } = true;
        public string Tags { get; set; }
        //public bool Recursive { get; set; } = true;
    }
}
