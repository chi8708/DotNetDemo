using Aspect;
using Model;
using Repository;
using Spring.Aop.Framework;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _02SpringAop
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 没有通过配置文件实现的aop
            Order order = new Order();
            order.Category = new Model.Category() { No = 2 };

            IOrderRepository target = new OrderRepository() { Order = order };
            ProxyFactory factory = new ProxyFactory(target);//OrderRepository 必须实现接口 ,InterfaceMap = Count 1

            factory.AddAdvice(new AfterAdviseOrder());
            factory.AddAdvice(new AroundAdviseOrder());
            //  factory.AddAdvice(new BeforeAdviseOrder());

            IOrderRepository manager = (IOrderRepository)factory.GetProxy();
            manager.Save(order);
            
            #endregion

            Console.ReadLine();
        }
    }
}
