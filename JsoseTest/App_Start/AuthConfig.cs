
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
namespace MvcApi.App_Start
{
    /// <summary>
    /// 添加token验证
    /// </summary>
    public class AuthConfig
    {
        public static void ClaimsBasedAuth()
        {
            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"fff"),
                new Claim(ClaimTypes.UserData, "{Name:'User',Age:12}")
            };
            var identity=new ClaimsIdentity(claim,"ttt");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            HttpContext.Current.User = principal;

        }
    }
}