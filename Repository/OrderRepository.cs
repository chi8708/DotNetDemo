using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
   public  class OrderRepository:IOrderRepository
    {
       public Order Order { get; set; }
       public void Save(Order order)
       {
          Console.WriteLine("数据层 order保存了===" + order.Category.No);
       }
    }
}
