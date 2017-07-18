using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public interface IOrderManage
    {
        public Order Order { get; set; }
        public void Save();
    }
}
