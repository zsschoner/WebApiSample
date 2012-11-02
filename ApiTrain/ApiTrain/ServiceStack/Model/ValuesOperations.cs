using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using CommonServer.Model;

namespace ApiServiceStack.ServiceModel.ValuesOperations
{
    #region ValueModel request contract

    [Route("api/users/{id}", "GET")]
    public class GetUserViewModel
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    /// When you use Route attribute, you need set the Content-Type header to a proper
    /// value, for example for JSON requests "Content-Type: application/json"
    /// otherwise routing will remove JSON content from the request body
    /// Other problem can be the invalid Data-Type header settings like in this post
    /// http://stackoverflow.com/questions/11678610/why-servicestack-could-not-make-model-binding-on-json-post-request
    /// </summary>
    [Route("api/users", "GET")]
    [Route("api/users", "POST")]
    [Route("api/users", "PUT")]
    [Route("api/users/{id}", "DELETE")]
    [Route("api/users/{id}/{UserName}/{Name}/{IsAnonymous}", "PUT")]
    public class UserViewModel : UserModel { }

    #endregion
}