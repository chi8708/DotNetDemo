using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTDemo.jwt1
{

    public class LoginViewModel
    {
        //[Required]
        public string Name { get; set; }

        //[Required]
        public string Password { get; set; }
    }
}
