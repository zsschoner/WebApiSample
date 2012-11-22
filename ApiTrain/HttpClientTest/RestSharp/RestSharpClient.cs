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

        private static IRestRequest CreateRequest(Method method)
        {
            var result = new RestRequest
                             {
                                 Method = method,
                                 JsonSerializer = new JsonSerializer { ContentType = "application/json" }
                             };
            result.AddHeader("Content-Type", "application/json");
            result.AddHeader("Accept", "application/json");
            result.RequestFormat = DataFormat.Json;

            return result;
        }

        public static void GetList()
        {
            var c = new RestClient(baseUrl);
            var resp = c.Get(CreateRequest(Method.GET));
            if (!Models.Any()) WriteResult("GET LIST", resp);

            Models.Clear();
            
            if(string.IsNullOrEmpty(resp.Content))
            {
                return;
            }
            
            var ds = new JsonDeserializer();
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

        public static void Get()
        {
            var id = GetSelectedId();

            if (id != Guid.Empty)
            {
                var c = new RestClient(String.Format("{0}/{1}", baseUrl, id));
                List<UserModel> res = Enumerable.Empty<UserModel>().ToList();
                c.GetAsync<List<UserModel>>(CreateRequest(RestSharp.Method.GET), (resp, result) =>
                {
                    //var ds = new JsonDeserializer();
                    res = resp.Data;
                    WriteResult("GET", resp);
                });
            }
        }

        public static void Post()
        {
            var c = new RestClient(baseUrl);
            List<UserModel> res = Enumerable.Empty<UserModel>().ToList();
            var model = new UserModel()
                            {
                                UserName = "User - " + Guid.NewGuid().ToString(),
                                Name = "RestClient user",
                                IsAnonymous = false
                            };

            c.PostAsync<UserModel>(CreateRequest(Method.POST).AddBody(model), (resp, result) => WriteResult("POST", resp));
        }

        public static void Put()
        {
            var id = GetSelectedId();

            if (id != Guid.Empty)
            {
                var c = new RestSharp.RestClient(String.Format("{0}", baseUrl));
                var model = Models.First(user => user.Id == id);
                model.Name = "RestClient user updated";

                c.PutAsync<UserModel>(CreateRequest(Method.PUT).AddBody(model), (resp, result) => WriteResult("PUT", resp));
            }
        }

        public static void Delete()
        {
            var id = GetSelectedId();

            if (id != Guid.Empty)
            {
                var c = new RestClient(String.Format("{0}/{1}", baseUrl, id));

                c.DeleteAsync<UserModel>(CreateRequest(Method.DELETE), (resp, result) => WriteResult("DELETE", resp));
            }
        }
    }
}
