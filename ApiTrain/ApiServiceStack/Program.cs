using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ApiServiceStack
{
    class Program
    {
        private const string ListeningOn = "http://localhost:82/";

		static void Main(string[] args)
		{
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(ListeningOn);

			System.Console.WriteLine("AppHost Created at {0}, listening on {1}",
				DateTime.Now, ListeningOn);

			var sb = new StringBuilder();
			sb.AppendLine("Base Url of service descriptor:\n");
            sb.AppendLine("WCF: " + "http://localhost:84/ApiService.svc/");
            sb.AppendLine("Mvc: " + "http://localhost:83/api/");
            sb.AppendLine("ServicesStack: " + ListeningOn);			

			System.Console.WriteLine(sb);			

			Thread.Sleep(Timeout.Infinite);
			System.Console.WriteLine("ReadLine()");
			System.Console.ReadLine();
		}
    }
}
