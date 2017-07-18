using JsoseTest.App_Start;
using JsoseTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsoseTest.Controllers
{
    [Authorize]
    [MyFilter]
    public class TestHTMLController : Controller
    {
        //
        // GET: /TestHTML/

        public ActionResult Index(int? status)
        {
            List<Product> liProduct = new List<Product>(){
               new Product{Status=1},
               new Product{Status=0}
           };

            ViewData["DDL"] = new List<SelectListItem>() { 
              new SelectListItem{Text="正常",Value="0"},
              new SelectListItem{Text="禁用",Value="1"}
            };
          
            return View(liProduct);
        }

        public ActionResult HtmlControl(int status) 
        {
            Product product = new Product() { Name = "testName" ,CreateTime=DateTime.Now,Remark="11111111111111111s33333333tttttte"};


            return View(product); 
        }

    }
}
