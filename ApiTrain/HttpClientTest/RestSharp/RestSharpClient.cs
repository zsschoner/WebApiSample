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
        private static List<UserModel> models = new List<UserModel>();

        /// <summary>
        /// Gets selected id from the console
        /// </summary>
        /// <returns></returns>
        private static Guid GetSelectedId()
        {
            Guid id = Guid.Empty;

            if (models.Any())
            {
                models.ForEach(model => Console.WriteLine(String.Format("Id{0} - UserName{1}", model.Id, model.UserName)));
                Console.WriteLine();
                Console.WriteLine("Paste id and press Enter");
                var str = Console.ReadLine();

                Guid.TryParse(str, out id);
            }
            return id;
        }

        private static IRestRequest CreateRequest(RestSharp.Method method)
        {
            var result = new RestRequest() { Method = method };
            result.JsonSerializer = new JsonSerializer();
            result.JsonSerializer.ContentType = "application/json";
            result.AddHeader("Content-Type", "application/json");
            result.AddHeader("Accept", "application/json");
            result.RequestFormat = DataFormat.Json;

            return result;
        }

        public static void GetList()
        {
            RestClient c = new RestSharp.RestClient(baseUrl);
            List<UserModel> res = Enumerable.Empty<UserModel>().ToList();
            var resp = c.Get(CreateRequest(RestSharp.Method.GET));

            var ds = new JsonDeserializer();
            var obj = ds.Deserialize<List<UserModel>>(resp);
            if (obj != null)
            {
                models.AddRange(obj);
            }

            Console.WriteLine(resp.Content);
        }

        public static void Get()
        {
            var id = GetSelectedId();

            if (id != Guid.Empty)
            {
                RestClient c = new RestSharp.RestClient(String.Format("{0}/{1}", baseUrl, id));
                List<UserModel> res = Enumerable.Empty<UserModel>().ToList();
                c.GetAsync<List<UserModel>>(CreateRequest(RestSharp.Method.GET), (resp, result) =>
                {
                    //var ds = new JsonDeserializer();
                    res = resp.Data;
                    Console.WriteLine(resp.Content);
                });
            }
        }

        public static void Post()
        {
            RestClient c = new RestSharp.RestClient(baseUrl);
            List<UserModel> res = Enumerable.Empty<UserModel>().ToList();
            var model = new UserModel()
            {
                UserName = "User - " + Guid.NewGuid().ToString(),
                Name = "RestClient user",
                IsAnonymous = false
            };

            c.PostAsync<UserModel>(CreateRequest(RestSharp.Method.POST).AddBody(model), (resp, result) =>
            {
                Console.WriteLine(resp.Content);
            });
        }

        public static void Put()
        {
            var id = GetSelectedId();

            if (id != Guid.Empty)
            {
                RestClient c = new RestSharp.RestClient(String.Format("{0}/{1}", baseUrl, id));
                var model = models.First(user => user.Id == id);
                model.Name = "RestClient user updated";

                c.PutAsync<UserModel>(CreateRequest(RestSharp.Method.PUT).AddBody(model), (resp, result) =>
                {
                    Console.WriteLine(resp.Content);
                });
            }
        }

        public static void Delete()
        {
            var id = GetSelectedId();

            if (id != Guid.Empty)
            {
                RestClient c = new RestSharp.RestClient(String.Format("{0}/{1}", baseUrl, id));

                c.DeleteAsync<UserModel>(CreateRequest(RestSharp.Method.DELETE), (resp, result) =>
                {
                    Console.WriteLine(resp.Content);
                });
            }
        }
    }
}
