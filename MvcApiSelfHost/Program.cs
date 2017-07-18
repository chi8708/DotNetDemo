using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.SelfHost;
using MvcApi.Controllers;

namespace MvcApiSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080");
            ApiConfig.Register(config);
     
            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }

    class ApiConfig 
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                    "API Default", "api/{controller}/{id}",
                   new { id = RouteParameter.Optional });
        }
    }
}
