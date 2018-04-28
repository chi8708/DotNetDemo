using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTest
{
    public class PageDateRep<T> where T:class,new()
    {
        public int code { get; set; }

        public string msg { get; set; }

        public int count { get; set; }

        public List<T> data { get; set; }
    }
}
