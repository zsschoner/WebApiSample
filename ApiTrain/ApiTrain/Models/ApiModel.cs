using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
using Common.Model;
using System.Net.Http;

namespace ApiMvc.Models
{
    /// <summary>
    /// Describes the api calls
    /// </summary>
    /// <remarks>
    /// The sample code can see on this page http://www.asp.net/web-api/overview/creating-web-apis/creating-a-help-page-for-a-web-api
    /// </remarks>
    public class ApiModel
    {
        private readonly IApiExplorer explorer_;

        public ApiModel(IApiExplorer explorer)
        {
            explorer_ = explorer;
        }

        /// <summary>
        /// Gets Apis from the explorer keyed by controller name
        /// </summary>
        /// <returns></returns>
        public ILookup<string, ApiDescription> GetApis()
        {
            return explorer_.ApiDescriptions.ToLookup(
                api => api.ActionDescriptor.ControllerDescriptor.ControllerName);
        }

        /// <summary>
        /// Data sample for users
        /// </summary>
        private static readonly List<UserModel> SampleUsers = new List<UserModel>(){
            new UserModel(){ Id = Guid.NewGuid(), UserName = "Sample user", Name = "Sample user", IsAnonymous = false},
            new UserModel(){ Id = Guid.NewGuid(), UserName = "Sample user 1", Name = "Sample user 1", IsAnonymous = true}
        };

        /// <summary>
        /// Data type - samle list
        /// </summary>
        private static readonly Dictionary<Type, object> Sample = new Dictionary<Type, object>()
        {
            {typeof(UserModel),SampleUsers[0]},
            {typeof(IQueryable<UserModel>),SampleUsers.AsQueryable()},
            {typeof(Guid), Guid.NewGuid()}

        };

        /// <summary>
        /// Gets a sample response for an api call
        /// </summary>
        /// <param name="api">The current api call</param>
        /// <param name="type">the type the parameter</param>
        /// <param name="mediaType">the media format information</param>
        /// <returns></returns>
        private string GetSampleResponseBody(ApiDescription api, Type type, string mediaType)
        {
            string body = null;

            object o;
            if (type != null && Sample.TryGetValue(type, out o))
            {
                var formatters = api.SupportedResponseFormatters;

                var formatter = formatters.FirstOrDefault(
                    f => f.SupportedMediaTypes.Any(m => m.MediaType == mediaType));

                if (formatter != null)
                {
                    var content = new ObjectContent(type, o, formatter);
                    body = content.ReadAsStringAsync().Result;
                }
            }
            return body;
        }

        /// <summary>
        /// Sample response overload
        /// </summary>
        /// <param name="api">api description</param>
        /// <param name="mediaType">media type format</param>
        /// <returns>the formatted result</returns>
        public string GetSampleResponseBody(ApiDescription api, string mediaType)
        {
            return GetSampleResponseBody(api, api.ActionDescriptor.ReturnType, mediaType);
        }

        /// <summary>
        /// Returns with a sample parameter
        /// </summary>
        /// <param name="api">the api</param>
        /// <param name="param">the parameter description</param>
        /// <param name="mediaType">the format of the result</param>
        /// <returns>the sample parameter</returns>
        public string GetParamSample(ApiDescription api, ApiParameterDescription param, string mediaType)
        {
            return GetSampleResponseBody(api, param.ParameterDescriptor.ParameterType, mediaType);
        }
    }
}