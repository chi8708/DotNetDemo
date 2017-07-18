using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JsoseTest.Controllers
{
    public class JsonPTestController : Controller
    {
        //
        // GET: /JsonPTest/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetByHC()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2843/");
            client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/TestApi").Result;
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsAsync<IEnumerable<object>>().Result;
            }
            return Content("");

        }
    }
}
