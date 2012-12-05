using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;

namespace ApiMvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Default API route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{uid}",
                defaults: new { uid = RouteParameter.Optional }
            );

            // Rest parameterized URL
            config.Routes.MapHttpRoute(
                name: "RestUserUpdate",
                routeTemplate: "api/{controller}/{id}/{username}/{name}/{isanonymous}",
                defaults: new { id = RouteParameter.Optional, controller = "User" }
            );

            // Adds JSON MediaTypeFormatter for text/html request
            // You can set almost everything through configuration
            var cfg = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            if (cfg != null)
            {
                cfg.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            }
        }
    }
}
