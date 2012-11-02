using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Common.Model;
using System.Net;

namespace HttpClientTest
{
    public class HttpClientHelper
    {
        private static List<UserModel> models = new List<UserModel>();
        private const string baseAddress = "http://localhost:83/customapi/";

        /// <summary>
        /// Gets the list of current users
        /// </summary>
        public static void GetList()
        {
            using (var client = new HttpClient())
            {
                var result = client.GetAsync(baseAddress);
                result.Result.EnsureSuccessStatusCode();
                var resultString = result.Result.Content.ReadAsString();

                var resultObj = ServiceStack.Text.Json.JsonReader<IEnumerable<UserModel>>.Parse(resultString) as IEnumerable<UserModel>;
                if (resultObj != null) models.AddRange(resultObj);
                Console.WriteLine(resultString);
            }
        }

        /// <summary>
        /// Gets a user
        /// </summary>
        public static void Get()
        {
            Guid id = GetSelectedId();
            if (id != Guid.Empty)
            {
                using (var client = new HttpClient())
                {
                    var result = client.GetAsync(String.Format("{0}{1}", baseAddress, id));

                    result.Result.EnsureSuccessStatusCode();
                    var resultString = result.Result.Content.ReadAsString();
                    var resultObj = ServiceStack.Text.Json.JsonReader<IEnumerable<UserModel>>.Parse(resultString) as IEnumerable<UserModel>;
                    Console.WriteLine(resultString);
                }
            }
        }

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

        /// <summary>
        /// Creates a user
        /// </summary>
        public static void Post()
        {
            Guid id = Guid.Empty;

            using (var client = new HttpClient())
            {
                var model = new UserModel()
                {
                    UserName = "User - " + Guid.NewGuid().ToString(),
                    Name = "HttpClient user",
                    IsAnonymous = false
                };
                HttpContent content = new StringContent(ServiceStack.Text.JsonSerializer.SerializeToString<UserModel>(model));

                var result = client.PostAsync(baseAddress, content);

                result.Result.EnsureSuccessStatusCode();
                var resultString = result.Result.Content.ReadAsString();
                var resultObj = ServiceStack.Text.Json.JsonReader<IEnumerable<UserModel>>.Parse(resultString) as IEnumerable<UserModel>;
                Console.WriteLine(resultString);
            }
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        public static void Put()
        {

            Guid id = GetSelectedId();

            if (id != Guid.Empty)
            {
                using (var client = new HttpClient())
                {
                    var model = models.First(user => user.Id == id);
                    model.Name = "HttpClient updated";
                    HttpContent content = new StringContent(ServiceStack.Text.JsonSerializer.SerializeToString<UserModel>(model));

                    var result = client.PutAsync(String.Format("{0}/{1}", baseAddress, id), content);

                    result.Result.EnsureSuccessStatusCode();
                    var resultString = result.Result.Content.ReadAsString();
                    var resultObj = ServiceStack.Text.Json.JsonReader<IEnumerable<UserModel>>.Parse(resultString) as IEnumerable<UserModel>;
                    Console.WriteLine(resultString);
                }
            }
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        public static void Delete()
        {

            Guid id = GetSelectedId();

            if (id != Guid.Empty)
            {
                using (var client = new HttpClient())
                {
                    var model = models.First(user => user.Id == id);

                    HttpContent content = new StringContent(ServiceStack.Text.JsonSerializer.SerializeToString<UserModel>(model));

                    var result = client.DeleteAsync(String.Format("{0}/{1}", baseAddress, id));

                    result.Result.EnsureSuccessStatusCode();
                    var resultString = result.Result.Content.ReadAsString();
                    var resultObj = ServiceStack.Text.Json.JsonReader<IEnumerable<UserModel>>.Parse(resultString) as IEnumerable<UserModel>;
                    Console.WriteLine(resultString);
                }
            }
        }
    }
}
