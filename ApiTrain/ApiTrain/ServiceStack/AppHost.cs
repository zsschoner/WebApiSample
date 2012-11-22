using ServiceStack.WebHost.Endpoints;
using ServiceStack.Common.Web;

namespace ServiceStack
{
    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost()
            : base("ServiceStack Examples", typeof(AppHost).Assembly)
        {

        }

        public override void Configure(Funq.Container container)
        {
            //Signal advanced web browsers what HTTP Methods you accept
            base.SetConfig(new EndpointHostConfig
            {
                GlobalResponseHeaders =
				{
					{ "Access-Control-Allow-Origin", "*" },
					{ "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
				},
                WsdlServiceNamespace = "http://www.servicestack.net/types",
                DefaultContentType = ContentType.Json
            });
        }
    }
}
