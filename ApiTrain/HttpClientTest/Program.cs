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

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                RestSharpClient.GetList();
                Console.WriteLine("=====  Menu  =====");
                Console.WriteLine("1 - Get user");
                Console.WriteLine("2 - Create user");
                Console.WriteLine("3 - Modify user");
                Console.WriteLine("4 - Delete user");
                Console.WriteLine("==================");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("Press a menu key");

                switch (Console.ReadKey().KeyChar)
                {
                    case exitChar: return;
                    case getUserChar: RestSharpClient.Get(); break;
                    case createUserChar: RestSharpClient.Post(); break;
                    case updateUserChar: RestSharpClient.Put(); break;
                    case deleteUserChar: RestSharpClient.Delete(); break;
                }

            } while (true);

        }
    }
}
