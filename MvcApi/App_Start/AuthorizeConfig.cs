using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MvcApi.App_Start
{
    public class AuthorizeConfig
    {
        public static void OAuth(IAppBuilder app)
        {
            var oauthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/accesstoken"),
                AccessTokenProvider = new TokenProvider(),
                RefreshTokenProvider = new RefreshTokenProvider(),
                Provider = new OauthProvider(),
              //  AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
                SystemClock = new SystemClock()
            };

            app.UseOAuthBearerTokens(oauthOptions);
        }
    }

    /// <summary>
    /// 重写token加密算法
    /// </summary>
    public class TokenProvider: AuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, string> _refreshTokens = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 生成 refresh_token
        /// </summary>
        public override void Create(AuthenticationTokenCreateContext context)
        {
            if (string.IsNullOrEmpty(context.Ticket.Identity.Name)) return;

            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(1);


            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _refreshTokens[context.Token] = context.SerializeTicket();
            context.SerializeTicket();
        }

        /// <summary>
        /// 由 refresh_token 解析成 access_token
        /// </summary>
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_refreshTokens.TryGetValue(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }
    }

    /// <summary>
    /// 刷新Token
    /// </summary>
    public class RefreshTokenProvider : AuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, string> _refreshTokens = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 生成 refresh_token
        /// </summary>
        public override void Create(AuthenticationTokenCreateContext context)
        {
            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(1);
           
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _refreshTokens[context.Token] = context.SerializeTicket();


        }

        /// <summary>
        /// 由 refresh_token 解析成 access_token
        /// </summary>
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_refreshTokens.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }
    }

    public class OauthProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// 验证客户端
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 验证用户名密码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "refreshToken"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

    }
}