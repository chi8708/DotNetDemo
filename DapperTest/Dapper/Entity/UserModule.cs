using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperTest.Dapper.Entity
{
    public class UserModule
    {
        public virtual int Userid { get; set; }
        public virtual int Moduleid { get; set; }

        public User User { get; set; }
    }
}