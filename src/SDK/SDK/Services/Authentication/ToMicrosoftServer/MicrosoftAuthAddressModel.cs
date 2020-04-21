﻿using Microsoft.AspNetCore.Mvc;

namespace Aiursoft.SDK.Services.Authentication.ToMicrosoftServer
{
    public class MicrosoftAuthAddressModel
    {
        [FromQuery(Name = "client_id")]
        public string ClientId { get; set; }
        [FromQuery(Name = "redirect_uri")]
        public string RedirectUri { get; set; }
        [FromQuery(Name = "state")]
        public string State { get; set; }
        [FromQuery(Name = "scope")]
        public string Scope { get; set; }
        [FromQuery(Name = "response_type")]
        public string ResponseType { get; set; }
    }
}