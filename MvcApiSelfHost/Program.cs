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
            var config = new HttpSelfHostConfiguration("http://localhost:9091");
            ApiConfig.Register(config);
     
            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
               var formatters= config.Formatters.Where(formatter =>
                   formatter.SupportedMediaTypes.Where(media =>
                   media.MediaType.ToString() == "application/xml" || media.MediaType.ToString() == "text/html").Count() > 0).ToList();

                foreach (var match in formatters)
                {
                    config.Formatters.Remove(match);  //移除请求头信息中的XML格式
                }
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }

    class ApiConfig 
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();//添加后RoutePrefix Route才有用
            config.Routes.MapHttpRoute(
                    "API Default", "api/{controller}/{id}",
                   new { id = RouteParameter.Optional });
        }
    }
}
