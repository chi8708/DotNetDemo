using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JsoseTest.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage="名称必填")]
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime?  CreateTime { get; set; }

        public int? Status { get; set; }
    }
}