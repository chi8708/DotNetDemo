using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperTest.Dapper.Entity
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Loginno { get; set; }
        public virtual string Password { get; set; }
        public virtual string Passport { get; set; }
        public virtual byte? Type { get; set; }
        public virtual int? Factoryid { get; set; }
        public virtual int? Roleid { get; set; }
        public virtual string Truename { get; set; }
        public virtual string Nickname { get; set; }
        public virtual byte? Gender { get; set; }
        public virtual string Company { get; set; }
        public virtual string Dept { get; set; }
        public virtual string Selfcellphone { get; set; }
        public virtual string Selftellphone { get; set; }
        public virtual string Duty { get; set; }
        public virtual string Address { get; set; }
        public virtual string Legal { get; set; }
        public virtual string Linkman { get; set; }
        public virtual string Cellphone { get; set; }
        public virtual string Tellphone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Postcode { get; set; }
        public virtual string Email { get; set; }
        public virtual string Qq { get; set; }
        public virtual string Businessimgpath { get; set; }
        public virtual string Taxregimgpath { get; set; }
        public virtual string Orgimgpath { get; set; }
        public virtual string Authno { get; set; }
        public virtual bool? Ifauth { get; set; }
        public virtual DateTime? Createtime { get; set; }
        public virtual DateTime? Editetime { get; set; }
        public virtual string Editeman { get; set; }
       // public virtual byte? Status { get; set; }

        public int MyProperty { get; set; }

        public IList<UserModule> Modules { get; set; }
    }
}