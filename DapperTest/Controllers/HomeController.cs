using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T4;

namespace DapperTest.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MsSqlDbHelper msHelper = new MsSqlDbHelper();
            var colunms= msHelper.GetDbColumns("");
            foreach (var item in colunms)
            {
               var i= item.ColumnName.Substring(0, 1).ToUpper() + item.ColumnName.Substring(1);
            }

            
            return null;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }


}