using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ApiWCF
{
    [ServiceContract]
    public interface IApiService
    {
        [OperationContract]
        [WebInvoke(Method="GET",
            ResponseFormat=WebMessageFormat.Json,
            BodyStyle=WebMessageBodyStyle.Wrapped,
            UriTemplate="values/")]
        IEnumerable<string> Values();
    }
}
