using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpClientTest
{
    class Program
    {
        private const char exitChar = '0';
        private const char getUserChar = '1';
        private const char createUserChar = '2';
        private const char updateUserChar = '3';
        private const char deleteUserChar = '4';
        private const char toggleFormatChar = '5';

        static void Main(string[] args)
        {
            var format = "xml";
            do
            {
                Console.Clear();
                RestSharpClient.GetList(format);
                Console.WriteLine("=====  Menu  =====");
                Console.WriteLine("1 - Get user");
                Console.WriteLine("2 - Create user");
                Console.WriteLine("3 - Modify user");
                Console.WriteLine("4 - Delete user");
                Console.WriteLine(String.Format("5 - Toggle format (current: {0})", format));
                Console.WriteLine("==================");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("Press a menu key");

                switch (Console.ReadKey().KeyChar)
                {
                    case exitChar: return;
                    case getUserChar: RestSharpClient.Get(format); break;
                    case createUserChar: RestSharpClient.Post(format); break;
                    case updateUserChar: RestSharpClient.Put(format); break;
                    case deleteUserChar: RestSharpClient.Delete(format); break;
                    case toggleFormatChar: format = format == "json" ? "xml" : "json"; break;
                }

            } while (true);

        }
    }
}
