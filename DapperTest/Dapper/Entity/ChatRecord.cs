using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperTest.Dapper.Entity
{
    public class ChatRecord
    {
        public virtual string Fromloginno { get; set; }
        public virtual string Tologinno { get; set; }
        public virtual byte? Type { get; set; }
        public virtual string Message { get; set; }
        public virtual DateTime? Sendtime { get; set; }
        public virtual byte? Status { get; set; }
    }
}