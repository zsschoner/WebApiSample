using System;
using ServiceStack.ServiceHost;
using Common.Model;

namespace ServiceStack.Model
{
    #region User request contract

    [Route("api/user/{id}", "GET")]
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
    [Route("api/user", "GET")]
    [Route("api/user", "POST")]
    [Route("api/user", "PUT")]
    [Route("api/user/{id}", "DELETE")]
    [Route("api/user/{id}/{UserName}/{Name}/{IsAnonymous}", "PUT")]
    public class UserViewModel : UserModel { }

    #endregion
}