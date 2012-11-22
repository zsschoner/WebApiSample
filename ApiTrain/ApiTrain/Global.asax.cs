using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ApiMvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private const string ServiceStackListeningOn = "http://localhost:82/";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Sets the configuration
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Starting ServiceStack
            StartServiceStack();
        }

        /// <summary>
        /// Starts servicestack api
        /// </summary>
        private static void StartServiceStack()
        {
            var appHost = new ServiceStack.AppHost();
            appHost.Init();
            appHost.Start(ServiceStackListeningOn);
        }
    }
}