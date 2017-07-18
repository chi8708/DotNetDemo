using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    class OrderService
    {

        CategoryService categoryBLL = new CategoryService();
        OrderRepository cateogryDAL = new OrderRepository();

        //传统业务逻辑也有点向AOP 对某个方法关注进行拦截
        public void Save(Order order) 
        {
            if (categoryBLL.CheckNo(order.Category))
            {
                cateogryDAL.Save(order);
            }
        }
    }
}
