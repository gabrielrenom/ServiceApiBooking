﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using ServiceAPI.Handlers;
using ACP.Business.Services;
using ACP.HMAC.Services;

namespace ServiceAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.MessageHandlers.Add(new HMACinHandler(
               new HMACService(
                               new HMACMessage(),
                               new SignatureManager())));

            config.MessageHandlers.Add(new HMACoutHandler(
                new HMACService(
                                new HMACMessage(),
                                new SignatureManager())));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
