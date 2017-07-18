using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    //目标代理 OrderRepository代理
    public class OrderManage:IOrderManage
    {
        public OrderRepository OrderDAL { get; set; }
        public void Save() 
        {
            Console.WriteLine("代理Save");

            OrderDAL.Save(Order);
        }
    }
}
