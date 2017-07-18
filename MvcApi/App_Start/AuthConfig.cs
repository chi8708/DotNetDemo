using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Infrastructure;
using System.Threading.Tasks;
using MvcApi.App_Start;
using System.Text;
using System.Web.Mvc;
using System.Collections.Concurrent;
namespace MvcApi.App_Start
{
    /// <summary>
    /// 添加token验证
    /// </summary>
    public class AuthConfig
    {

        //OAuth身份验证
        public static  void OAuth(IAppBuilder app) 
        {

            var oauthProvider = new OAuthAuthorizationServerProvider
            {

                OnGrantResourceOwnerCredentials = async context =>
                {
                    if (context.UserName == "rranjan" && context.Password == "password@123")
                    {
                        var claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                        claimsIdentity.AddClaim(new Claim("user", context.UserName));
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "ApiUser"));
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, "{Name:'User',Age:12}"));
                        context.Validated(claimsIdentity);
                        return;
                    }
                    context.Rejected();
                },
                OnValidateClientAuthentication = async context =>
                {
                    string clientId;
                    string clientSecret;
                    if (context.TryGetBasicCredentials(out clientId, out clientSecret))
                    {
                        if (clientId == "rajeev" && clientSecret == "secretKey")
                        {
                            context.Validated();
                        }
                    }
                    else if (context.TryGetFormCredentials(out clientId, out clientSecret))
                    {
                        if (clientId == "rajeev" && clientSecret == "secretKey")
                        {
                            context.Validated();
                        }
                    }
                    //context.Validated();
                }
            };

            var oauthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/accesstoken"),
                Provider = oauthProvider,
                AuthorizationCodeExpireTimeSpan = TimeSpan.FromMinutes(1),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(3),
                SystemClock = new SystemClock()

            };

            app.UseOAuthAuthorizationServer(oauthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
        }

        public static void FormAuth(IAppBuilder app) 
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "fff", DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), true, FormsAuthentication.FormsCookiePath);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = ticket.CookiePath;
            HttpContext.Current.Response.Cookies.Add(cookie);
          // FormsAuthentication.RedirectFromLoginPage("ffff", false);
        
        }

        public static void ClaimsBasedAuth()
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, "Chi"));

            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            var ctx =HttpContext.Current.Request.GetOwinContext();

            var authenticationManager = ctx.Authentication;

            authenticationManager.SignIn(id);

            //或
            //ClaimsPrincipal principal = new ClaimsPrincipal(id);
            //HttpContext.Current.User = principal;
            
        }

    }


}

public class TokenProvider : AuthenticationTokenProvider
{

    public override void Create(AuthenticationTokenCreateContext context)
    {

        context.SetToken("ffffsss");//可以重写token算法
        base.Create(context);
    }

    public override System.Threading.Tasks.Task CreateAsync(AuthenticationTokenCreateContext context)
    {
        context.SetToken(context.SerializeTicket());//可以重写token算法
        return base.CreateAsync(context);
    }
    public void Receive(AuthenticationTokenReceiveContext context)
    {
        context.DeserializeTicket(context.Token);

    }

}


public class Auth2Config
{


    public static void OAuth(IAppBuilder app)
    {
        var oauthOptions = new OAuthAuthorizationServerOptions
        {
            AllowInsecureHttp = true,
            TokenEndpointPath = new PathString("/accesstoken"),
            AccessTokenProvider = new RefreshTokenProvider(),
            RefreshTokenProvider=new RefreshTokenProvider(),
            Provider = new OauthProvider(),
            AuthorizationCodeExpireTimeSpan = TimeSpan.FromMinutes(1),
            AccessTokenExpireTimeSpan = TimeSpan.FromHours(3),
            SystemClock = new SystemClock()
        };

        app.UseOAuthBearerTokens(oauthOptions);
        //app.UseOAuthAuthorizationServer(oauthOptions);
        //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());



    }

    public static Action<AuthenticationTokenCreateContext> create = new Action<AuthenticationTokenCreateContext>(c =>
    {
        c.SetToken(c.SerializeTicket());
    });

    public static Action<AuthenticationTokenReceiveContext> receive = new Action<AuthenticationTokenReceiveContext>(c =>
    {
        c.DeserializeTicket(c.Token);
        c.OwinContext.Environment["Properties"] = c.Ticket.Properties;
    });

}


public class OauthProvider : OAuthAuthorizationServerProvider
{
    public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
        string clientId;
        string clientSecret;
        if (context.TryGetBasicCredentials(out clientId, out clientSecret))
        {
            if (clientId == "rajeev" && clientSecret == "secretKey")
            {
                context.Validated();
            }
        }
        else if (context.TryGetFormCredentials(out clientId, out clientSecret))
        {
            if (clientId == "rajeev" && clientSecret == "secretKey")
            {
                context.Validated();
            }
        }
        return base.ValidateClientAuthentication(context);
    }

    public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {
        if (context.UserName == "rranjan" && context.Password == "password@123")
        {
            var claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            claimsIdentity.AddClaim(new Claim("user", context.UserName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "ApiUser"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, "{Name:'User',Age:12}"));

            context.Validated(claimsIdentity);
        }
        else
        {
            context.Rejected();
        }
        return base.GrantResourceOwnerCredentials(context);
    }

}


