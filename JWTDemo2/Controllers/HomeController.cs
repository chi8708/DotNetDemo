using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using JWTDemo2.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace JWTDemo2.Controllers
{
    public class HomeController : ApiController
    {
        public LoginResult Post([FromBody]LoginRequest request)
        {
            LoginResult rs = new LoginResult();
            //这是是获取用户名和密码的，这里只是为了模拟
            if (request.UserName == "cts" && request.Password == "123")
            {
                AuthInfo info = new AuthInfo { UserName = "wangshibang", Roles = new List<string> { "Admin", "Manage" }, IsAdmin = true };
                try
                {
                    const string secret = "To Live is to change the world";
                    //secret需要加密
                    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                    IJsonSerializer serializer = new JsonNetSerializer();
                    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
                    var token = encoder.Encode(info, secret);
                    rs.Message = "XXXXX";
                    rs.Token = token;
                    rs.Success = true;
                }
                catch (Exception ex)
                {
                    rs.Message = ex.Message;
                    rs.Success = false;
                }
            }
            else
            {
                rs.Message = "fail";
                rs.Success = false;
            }
            return rs;
        }
    }
}