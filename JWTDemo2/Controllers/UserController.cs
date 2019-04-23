using JWTDemo2.Attributes;
using JWTDemo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JWTDemo2.Controllers
{

        [ApiAuthorize]
        public class UserController : ApiController
        {
            public string Get()
            {
                //获取回用户信息(在ApiAuthorize中通过解析token的payload并保存在RouteData中)
                AuthInfo authInfo = this.RequestContext.RouteData.Values["auth"] as AuthInfo;
                if (authInfo == null)
                    return "无效的验收信息";
                else
                    return string.Format("你好:{0},成功取得数据", authInfo.UserName);
            }
        }
    
}
