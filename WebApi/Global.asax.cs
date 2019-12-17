﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.All;
        }
        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        }
        protected void Session_Start(Object sender, EventArgs e)
        {
            if (Response.Cookies.Count > 0)
            {

                foreach (string s in Response.Cookies.AllKeys)
                {

                    if (s == System.Web.Security.FormsAuthentication.FormsCookieName ||

                        s.ToLower().Equals("asp.net_sessionid"))
                    {

                        Response.Cookies[s].HttpOnly = false;
                    }
                }
            }
        }
        void Application_EndRequest(object sender, EventArgs e)
        {

            if (Response.Cookies.Count > 0)
            {

                foreach (string s in Response.Cookies.AllKeys)
                {

                    Response.Cookies[s].HttpOnly = false;
                }

            }

        }
    }
}
