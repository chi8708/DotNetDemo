using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JsoseTest.Controllers
{
    public class lhgDialogController : Controller
    {
        //
        // GET: /lhgDialog/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChildPage() 
        {
            return View();
        }

    }
}
