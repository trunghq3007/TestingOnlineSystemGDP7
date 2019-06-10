using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Cors;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static string UrlPrefix { get { return "api"; } }
        public static string UrlPrefixRelative { get { return "~/api"; } }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.EnableCors();
            //var cors = new CorsPolicy
            //{
            //    AllowAnyMethod = true,
            //    AllowAnyHeader = true,
            //    AllowAnyOrigin = true,
            //};
            
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-zip-compressed"));
            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: WebApiConfig.UrlPrefix + "/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
        }
    }
}
