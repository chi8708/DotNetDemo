using JWT;
using JWT.Serializers;
using JWTDemo2.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JWTDemo2.Attributes
{
    /// <summary>
    /// 参考 https://www.cnblogs.com/wangyulong/p/8734495.html
    /// </summary>
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authHeader = from t in actionContext.Request.Headers where t.Key == "auth" select t.Value.FirstOrDefault();
            if (authHeader != null)
            {
                string token = authHeader.FirstOrDefault();
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        const string secret = "To Live is to change the world";
                        //secret需要加密
                        IJsonSerializer serializer = new JsonNetSerializer();
                        IDateTimeProvider provider = new UtcDateTimeProvider();
                        IJwtValidator validator = new JwtValidator(serializer, provider);
                        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                        IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                        var json = decoder.DecodeToObject<AuthInfo>(token, secret, verify: true);
                        if (json != null)
                        {
                            actionContext.RequestContext.RouteData.Values.Add("auth", json);
                            return true;
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}