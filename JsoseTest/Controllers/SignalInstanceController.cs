using JsoseTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsoseTest.Controllers
{
    //单例模式
    public class SignalInstanceController : Controller
    {
        //
        // GET: /SignalInstance/

        public ActionResult Index()
        {
            Signal data = null;

            for (int i = 0; i < 2; i++)
            {
                data = Signal.SignalInstance3;
            }

          
           
            string str= People.str;
           string str2 = People.str;
            return View();
        }



    }
}
