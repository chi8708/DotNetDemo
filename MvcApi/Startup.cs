using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MvcApi.App_Start;
using Owin;
using System;
using System.Security.Claims;
using System.Web.Http;

[assembly: OwinStartup(typeof(MvcApi.Startup))]
namespace MvcApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //HttpConfiguration config = new HttpConfiguration();
            //WebApiConfig.Register(config);
            //app.UseWebApi(config);
          //  Auth2Config.OAuth(app);
            //AuthConfig.ClaimsBasedAuth(app);

            AuthorizeConfig.OAuth(app);
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            //app.UseCookieAuthentication(new CookieAuthenticationOptions

            //{

            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,

            //    LoginPath = new PathString("/api/Values"),

            //    CookieSecure = CookieSecureOption.Never,
            //    CookieName = "Application"

            //});
        }

    }
}