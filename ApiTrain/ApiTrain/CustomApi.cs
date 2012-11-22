using System;
using System.Linq;
using System.Web;
using Common.Model;
using System.Text;
using System.Web.Helpers;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace ApiMvc
{
    /// <summary>
    /// HttpHandler sample for Rest Web Service
    /// </summary>
    public class CustomApi : IHttpHandler
    {
        // Format specifiers
        private const string XmlFormat = "application/xml";
        private static Data.IUserOperations Repository;

        public CustomApi()
        {
            Repository = Data.UserOperations.Instance;
        }

        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// IHttpHandler processrequest
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            Object result = null;
            // Routing request based on HttpMethod
            switch (context.Request.HttpMethod)
            {
                case "GET": result = Get(context); ; break;
                case "POST": result = Create(context); break;
                case "PUT": result = Update(context); break;
                case "DELETE": result = Delete(context); break;
                default: ; break;
            }

            // Returns the result
            context.Response.Write(Serialize(context, result));
        }

        /// <summary>
        /// Processes get request
        /// </summary>
        /// <param name="context"></param>
        private static object Get(HttpContext context)
        {
            return Get(GetIdFromRequest(context));
        }

        /// <summary>
        /// Gets Id from context routepart or query string parameter named by id
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Guid GetIdFromRequest(HttpContext context)
        {
            var id = Guid.Empty;

            // Getting id from query string
            var idStr = context.Request.QueryString["id"];

            if (!String.IsNullOrEmpty(idStr))
            {
                Guid.TryParse(idStr, out id);
            }

            // if id is not in the query string
            if (id == Guid.Empty)
            {
                var startPos = context.Request.RawUrl.IndexOf(ServiceConstants.HttpHandlerRoot, System.StringComparison.Ordinal) + ServiceConstants.HttpHandlerRoot.Length;
                var routepart = context.Request.RawUrl.Substring(startPos, context.Request.RawUrl.Length - startPos);
                if (!string.IsNullOrEmpty(routepart))
                {
                    Guid.TryParse(routepart.Trim(), out id);
                }
            }

            return id;
        }

        /// <summary>
        /// Deserializes the request
        /// </summary>
        /// <param name="context">the current context</param>
        /// <returns>the deserialized UserModel if any</returns>
        private static UserModel Deserialize(HttpContext context)
        {
            byte[] request = context.Request.BinaryRead(context.Request.ContentLength);

            // Handles json and xml format, by default uses Json
            var json = true;
            var format = context.Request.ContentType;
            if (!String.IsNullOrEmpty(format))
            {
                if (format.Contains(XmlFormat))
                {
                    json = false;
                }
            }

            switch (json)
            {
                case true: return Json.Decode<UserModel>(context.Request.ContentEncoding.GetString(request));

                default:
                    var ds = new XmlSerializer(typeof(UserModel));
                    using (var ms = new MemoryStream(request))
                    {
                        return (UserModel)ds.Deserialize(ms);
                    }
            }
        }

        /// <summary>
        /// Serializes the model
        /// </summary>
        /// <param name="context">current Http context </param>
        /// <param name="model">model to serialize</param>
        /// <returns>returns with the serialized data</returns>
        private static string Serialize(HttpContext context, object model)
        {
            string result;
            bool json = !(context.Request.AcceptTypes != null
                          && context.Request.AcceptTypes.Contains(XmlFormat));

            if (json)
            {
                result = Json.Encode(model);
            }
            else
            {
                var sr = new XmlSerializer(model.GetType());
                using (var ms = new MemoryStream())
                {
                    var writer = new XmlTextWriter(ms, Encoding.UTF8);
                    sr.Serialize(writer, model);

                    result = Encoding.UTF8.GetString(ms.ToArray());
                }
            }

            return result;
        }

        /// <summary>
        /// Makes Get request result
        /// </summary>
        /// <param name="id"></param>
        private static object Get(Guid id)
        {
            object result = null;

            if (id == Guid.Empty)
            {
                result = Repository.List();
            }
            else
            {
                result = Repository.Get(id);
            }

            return result;
        }

        /// <summary>
        /// Creates new user and returns with it
        /// </summary>
        /// <param name="context">current http context</param>
        /// <returns>the created user</returns>
        private static object Create(HttpContext context)
        {
            return Repository.Create(Deserialize(context));
        }

        /// <summary>
        /// Updates user and resturn with updated data
        /// </summary>
        /// <param name="context">current http context</param>
        /// <returns>the updated user</returns>
        private static object Update(HttpContext context)
        {
            return Repository.Update(GetIdFromRequest(context), Deserialize(context));
        }

        /// <summary>
        /// Removes user and returns with removed user
        /// </summary>
        /// <param name="context">current http context</param>
        /// <returns>the deleted user</returns>
        private static object Delete(HttpContext context)
        {
            return Repository.Delete(new UserModel() { Id = GetIdFromRequest(context) });
        }
    }
}