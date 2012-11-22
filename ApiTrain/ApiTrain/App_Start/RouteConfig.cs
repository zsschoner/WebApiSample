using System.Web.Mvc;
using System.Web.Routing;

namespace ApiMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // skips resources
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // skips wcf services
            routes.IgnoreRoute("{resource}.svc/{*pathInfo}");
            // Skips customapi routes
            routes.IgnoreRoute(ServiceConstants.HttpHandlerRoot + "{*pathInfo}");

            // All request to user root wihtout parameter will be serviced with UserMvc.List() call
            routes.MapRoute(
                name: "Root",
                url: "user",
                defaults: new { controller = "UserMvc", action = "List" }
            );
            
            // All request to user root with a parameter will be serviced with an Index call
            routes.MapRoute(
                name: "IdCommand",
                url: "user/{id}",
                defaults: new { controller = "UserMvc", action = "Index", id = UrlParameter.Optional }
            );

            // Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}