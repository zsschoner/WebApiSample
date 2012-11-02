using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //HttpClientHelper.GetList();
            //HttpClientHelper.Delete();

            RestSharpClient.GetList();
            RestSharpClient.Get();

            Console.WriteLine();
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();

        }
    }
}
