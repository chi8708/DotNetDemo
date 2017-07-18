using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsoseTest
{
    
    public class KnockOutJSController : Controller
    {
        //
        // GET: /KnockOutJS/
        IList<Member> members = new List<Member> {
            new Member{name="jjack",age=19,hobbies=new string[]{"football","run"},gender=0},
            new Member{name="jrose",age=20}
        };
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JsonMappingHandler() 
        {
            return View();
        
        }

        [HttpPost]
        public JsonResult GetAllMember() 
        {
            return Json(new {first=members[0],people=members});
        }

    }

    public class Member 
    {
        public string name { get; set; }
        public int age { get; set; }

        public string[] hobbies { get; set; }

        public byte gender { get; set; }
    }

}
