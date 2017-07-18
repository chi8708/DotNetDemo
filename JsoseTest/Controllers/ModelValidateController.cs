using JsoseTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsoseTest.Controllers
{
    public class ModelValidateController : Controller
    {
        //
        // GET: /ModelValidate/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Product product, FormCollection form) 
        {
            var isValidate = ModelState.IsValid;
            return null;
        }

    }
}
