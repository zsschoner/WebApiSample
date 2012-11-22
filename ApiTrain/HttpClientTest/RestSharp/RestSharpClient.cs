using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Common.Model;
using RestSharp.Serializers;
using RestSharp.Deserializers;

namespace HttpClientTest
{
    public class RestSharpClient
    {
        private const string baseUrl = "http://localhost:82/api/user";
        private static readonly List<UserModel> Models = new List<UserModel>();

        /// <summary>
        /// Gets selected id from the console
        /// </summary>
        /// <returns></returns>
        private static Guid GetSelectedId()
        {
            var id = Guid.Empty;

            if (Models.Any())
            {
                Console.WriteLine("Id - UserName");
                Models.ForEach(model => Console.WriteLine(String.Format("{0} - {1}", model.Id, model.UserName)));
                Console.WriteLine();
                Console.WriteLine("Paste id and press Enter");
                var str = Console.ReadLine();

                Guid.TryParse(str, out id);
            }
            return id;
        }

        private static IRestRequest CreateRequest(Method method, string format)
        {
            var result = new RestRequest
                             {
                                 Method = method,
                                 JsonSerializer = new JsonSerializer { ContentType = "application/json; charset=utf-8" },
                                 XmlSerializer = new XmlSerializer() { ContentType = "application/xml; charset=utf-8" }
                             };

            result.AddHeader("Content-Type", String.Format("application/{0}; charset=utf-8", format));
            result.AddHeader("Accept", String.Format("application/{0}; charset=utf-8", format));
            result.RequestFormat = format == "json" ? DataFormat.Json : DataFormat.Xml;

            return result;
        }

        public static void GetList(string format)
        {
            var c = new RestClient(String.Format("{0}?format={1}", baseUrl, format));
            var resp = c.Get(CreateRequest(Method.GET, format));
            if (!Models.Any()) WriteResult("GET LIST", resp);

            Models.Clear();

            if (string.IsNullOrEmpty(resp.Content))
            {
                return;
            }

            var ds = format == "json" ? new JsonDeserializer() as IDeserializer : new XmlDeserializer() as IDeserializer;
            
            if (format != "json") resp.Content = resp.Content.Substring(1);
            var obj = ds.Deserialize<List<UserModel>>(resp);

            if (obj != null)
            {
                Models.AddRange(obj);
            }

        }

        private static void WriteResult(String method, IRestResponse resp)
        {
            Console.WriteLine(String.Format("==== {0} Result =====", resp.Request != null ? resp.Request.Method.ToString() : method));
            Console.WriteLine(resp.Content);
            Console.WriteLine("=================");
        }

        public static void Get(string format)
        {
            var id = GetSelectedId();

            if (id != Guid.Empty)
            {
                var c = new RestClient(String.Format("{0}/{1}?format={2}", baseUrl, id, format));
                List<UserModel> res = Enumerable.Empty<UserModel>().ToList();
                c.GetAsync<List<UserModel>>(CreateRequest(RestSharp.Method.GET, format), (resp, result) =>
                {
                    res = resp.Data;
                    WriteResult("GET", resp);
                });
            }
        }

        public static void Post(string format)
        {
            var c = new RestClient(baseUrl);
            
            var model = new UserResource()
                            {
                                UserName = "User - " + Guid.NewGuid().ToString(),
                                Name = "RestClient user",
                                IsAnonymous = false
                            };

            c.PostAsync<UserResource>(CreateRequest(Method.POST, "json").AddBody(model), (resp, result) => WriteResult("POST", resp));
        }

        public static void Put(string format)
        {
            var id = GetSelectedId();

            if (id != Guid.Empty)
            {
                var c = new RestSharp.RestClient(String.Format("{0}", baseUrl));
                var model = Models.First(user => user.Id == id);
                model.Name = "RestClient user updated";

                c.PutAsync<UserModel>(CreateRequest(Method.PUT, "json").AddBody(model), (resp, result) => WriteResult("PUT", resp));
            }
        }

        public static void Delete(string format)
        {
            var id = GetSelectedId();

            if (id != Guid.Empty)
            {
                var c = new RestClient(String.Format("{0}/{1}", baseUrl, id));

                c.DeleteAsync<UserResource>(CreateRequest(Method.DELETE, format), (resp, result) => WriteResult("DELETE", resp));
            }
        }
    }
}
