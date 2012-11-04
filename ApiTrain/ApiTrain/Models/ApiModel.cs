using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using Common.Model;
using System.Net.Http.Formatting;
using System.Net.Http;

namespace ApiMvc.Models
{
    public class ApiModel
    {
        private IApiExplorer explorer;

        public ApiModel(IApiExplorer explorer)
        {
            this.explorer = explorer;
        }

        public ILookup<string, ApiDescription> GetApis()
        {
            return explorer.ApiDescriptions.ToLookup(
                api => api.ActionDescriptor.ControllerDescriptor.ControllerName);
        }

        private static List<UserModel> sampleUsers = new List<UserModel>(){
            new UserModel(){ Id = Guid.NewGuid(), UserName = "Sample user", Name = "Sample user", IsAnonymous = false},
            new UserModel(){ Id = Guid.NewGuid(), UserName = "Sample user 1", Name = "Sample user 1", IsAnonymous = true}
        };

        private static Dictionary<Type, object> sample = new Dictionary<Type, object>()
        {
            {typeof(UserModel),sampleUsers[0]},
            {typeof(IEnumerable<UserModel>),sampleUsers},
            {typeof(Guid), Guid.NewGuid()}

        };

        private string GetSampleResponseBody(ApiDescription api, Type type, string mediaType)
        {
            string body = null;

            object o;
            if (type != null && sample.TryGetValue(type, out o))
            {
                var formatters = api.SupportedResponseFormatters;

                MediaTypeFormatter formatter = formatters.FirstOrDefault(
                    f => f.SupportedMediaTypes.Any(m => m.MediaType == mediaType));

                if (formatter != null)
                {
                    var content = new ObjectContent(type, o, formatter);
                    body = content.ReadAsStringAsync().Result;
                }
            }
            return body;
        }

        public string GetSampleResponseBody(ApiDescription api, string mediaType)
        {
            return GetSampleResponseBody(api, api.ActionDescriptor.ReturnType, mediaType);
        }

        public string GetParamSample(ApiDescription api, ApiParameterDescription param, string mediaType)
        {
            return GetSampleResponseBody(api, param.ParameterDescriptor.ParameterType, mediaType);
        }
    }
}