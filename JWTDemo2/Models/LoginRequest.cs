using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWTDemo2.Models
{
    public class LoginRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}