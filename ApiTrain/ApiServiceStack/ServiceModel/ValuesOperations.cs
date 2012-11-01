using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using CommonServer.Model;

namespace ApiServiceStack.ServiceModel.ValuesOperations
{
    #region Values

    [Route("api/values", "GET")]
    public class Values { }

    public class ListValuesResponse
    {
        public IQueryable<ValueModel> Result { get; set; }
    }

    #endregion

    #region GetValue

    [Route("api/values/{id}", "GET")]
    public class GetValue
    {
        public int Id { get; set; }
    }

    public class GetValueResponse
    {
        public ValueModel Result { get; set; }
    }

    #endregion

    #region Create

    [Route("api/values", "POST")]
    public class PostValue
    {
        public int Id { get; set; }
        public string Name { get; set; }        
    }

    public class PostValueResponse { }

    #endregion

    #region Update

    [Route("api/values/{id}", "PUT")]
    public class PutValue : ValueModel { }

    public class PutValueResponse : ValueModel { }

    #endregion

    #region Delete

    [Route("api/values/{id}", "DELETE")]
    public class DeleteValue : ValueModel { }

    public class DeleteValueResponse : ValueModel { }

    #endregion

    /// <summary>
    /// When you use Route attribute, you need set the Content-Type header to a proper
    /// value, for example for JSON requests "Content-Type: application/json"
    /// otherwise routing will remove JSON content from the request
    /// Other problem can be the invalid Data-Type header settings like in this post
    /// http://stackoverflow.com/questions/11678610/why-servicestack-could-not-make-model-binding-on-json-post-request
    /// </summary>
    [Route("api/values", "GET")]
    [Route("api/values", "POST")]
    [Route("api/values", "PUT")]
    [Route("api/values/{id}", "DELETE")]
    public class ValuesDefault
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
