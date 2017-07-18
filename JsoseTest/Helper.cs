using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

 namespace System.Web.Mvc
{
    public static class HelperExt
    {

        public static MvcHtmlString LableExt(this HtmlHelper helper, string value)
        {
            return new MvcHtmlString(string.Format("<span style='color:red'>{0}</span>",value));
        }
    }
}