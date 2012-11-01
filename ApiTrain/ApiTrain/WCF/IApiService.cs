﻿using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using CommonServer.Model;
using System.Linq;
using System;

namespace ApiWCF
{
    [ServiceContract]
    public interface IApiService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "values/")]
        IEnumerable<ValueModel> Values();
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "values/{id}")]
        ValueModel Get(string id);        
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "values/")]
        ValueModel Post(ValueModel model);
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "values?id={id}")]
        ValueModel Put(int id, ValueModel model);
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "values/")]
        ValueModel Delete(ValueModel model);
    }
}