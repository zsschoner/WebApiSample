using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http.Formatting;
using ApiMvc.Controllers;

namespace ApiMvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{uid}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "RestUserUpdate",
                routeTemplate: "api/{controller}/{id}/{username}/{name}/{isanonymous}",
                defaults: new { id = RouteParameter.Optional, controller = "User" }
            );

            // Adds JSON MediaTypeFormatter for text/html request
            var cfg = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            if (cfg != null)
            {
                cfg.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            }
            
        }
    }
}
