using System.Web;
using System.Web.Mvc;
using JsoseTest.App_Start;

namespace JsoseTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MyFilterAttribute());
        }
    }
}