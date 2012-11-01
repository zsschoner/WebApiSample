using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http.Formatting;

namespace ApiMvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
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
