using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JsoseTest.App_Start
{
   public class MyFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller;
            string  action = filterContext.RequestContext.RouteData.Values["action"].ToString();

            var type = controller.GetType();
            var isClassAttr= type.CustomAttributes.Any(p => p.AttributeType == typeof(MyFilterAttribute));
            var name = type.GetMethod(action);
            var pms = filterContext.HttpContext.Request;
        }
    }
}
