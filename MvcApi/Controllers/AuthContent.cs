using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MvcApi.Controllers
{
    public class AuthContent:IdentityDbContext<IdentityUser>
    {
        public AuthContent()
            : base("AuthContent") 
        {
        }
    }
}
