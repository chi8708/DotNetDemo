using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWTDemo2.Models
{
    public class AuthInfo
    {
        //模拟JWT的payload
        public string UserName { get; set; }

        public List<string> Roles { get; set; }

        public bool IsAdmin { get; set; }
    }
}