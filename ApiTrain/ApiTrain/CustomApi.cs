using System;
using System.Collections.Generic;
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
    public class CustomApi : IHttpHandler
    {
        private const string JsonFormat = "application/json";
        private const string XmlFormat = "application/xml";
        private const string HtmlFormat = "text/html";

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            Object result = null;
            switch (context.Request.HttpMethod)
            {
                case "GET": result = Get(context); ; break;
                case "POST": result = Create(context); break;
                case "PUT": result = Update(context); break;
                case "DELETE": result = Delete(context); break;
                default: ; break;
            }

            context.Response.Write(Serialize(context, result));
        }

        /// <summary>
        /// Processes get request
        /// </summary>
        /// <param name="context"></param>
        private object Get(HttpContext context)
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

            if (context.Request.QueryString != null)
            {
                var idStr = context.Request.QueryString["id"];

                if (!String.IsNullOrEmpty(idStr))
                {
                    Guid.TryParse(idStr, out id);
                }
            }

            if (id == Guid.Empty)
            {
                var startPos = context.Request.RawUrl.IndexOf("customapi/") + "customapi/".Length;
                var routepart = context.Request.RawUrl.Substring(startPos, context.Request.RawUrl.Length - startPos);
                if (!string.IsNullOrEmpty(routepart))
                {
                    Guid.TryParse(routepart.Trim(), out id);
                }
            }

            return id;
        }

        private static UserModel Deserialize(HttpContext context)
        {
            byte[] request = context.Request.BinaryRead(context.Request.ContentLength);

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
                    MemoryStream ms = new MemoryStream(request);

                    return (UserModel)ds.Deserialize(ms);
                    
            }
        }

        private static string Serialize(HttpContext context, object model)
        {
            string result = String.Empty;
            var json = true;
            if (context.Request.AcceptTypes != null
               && context.Request.AcceptTypes.Contains(XmlFormat))
            {
                json = false;
            }

            if (json)
            {
                result = Json.Encode(model);
            }
            else
            {
                XmlSerializer sr = new XmlSerializer(model.GetType());
                var ms = new MemoryStream();

                XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
                sr.Serialize(writer, model);
                ms = (MemoryStream)writer.BaseStream;
                result = Encoding.UTF8.GetString(ms.ToArray());
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
                result = Data.UserOperations.List();
            }
            else
            {
                result = Data.UserOperations.Get(id);
            }

            return result;
        }

        /// <summary>
        /// Creates new user and returns with it
        /// </summary>
        /// <param name="value"></param>
        private static object Create(HttpContext context)
        {
            return Data.UserOperations.Create(Deserialize(context));
        }

        /// <summary>
        /// Updates user and resturn with updated data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        private static object Update(HttpContext context)
        {
            return Data.UserOperations.Update(GetIdFromRequest(context), Deserialize(context));
        }

        /// <summary>
        /// Removes user and returns with removed user
        /// </summary>
        /// <param name="id"></param>
        private static object Delete(HttpContext context)
        {
            return Data.UserOperations.Delete(new UserModel() { Id = GetIdFromRequest(context) });
        }
    }
}