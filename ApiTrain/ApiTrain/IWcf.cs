using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Common.Model;

namespace WCF
{
    /// <summary>
    /// Service contract for sample Wcf REST implementation
    /// </summary>
    [ServiceContract]
    public interface IWcf
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "user/")]
        IEnumerable<UserModel> GetList();
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "user/{id}")]
        UserModel Get(string id);        
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "user/")]
        UserModel Post(UserModel model);
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "user/{id}")]
        UserModel Put(string id, UserModel model);
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "user/{id}/{username}/{name}/{isanonymous}")]
        UserModel PutUri(string id, string username, string name, string isAnonymous);
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "user/{id}")]
        UserModel Delete(string id);
    }
}
