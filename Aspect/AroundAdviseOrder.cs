using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop;
using AopAlliance.Aop;
using AopAlliance.Intercept;
using Repository;
using Model;
using System.Reflection;

namespace Aspect
{
    public class AroundAdviseOrder:IMethodInterceptor,IAfterReturningAdvice
    {
        //注意同一个类四种拦截方式不能共同使用 即不能同时继承几个接口 。如上 只会实现IMethodInterceptor接口对象的方法
        //解决方法：不同的拦截要使用不同的类，然后把拦截加入代理工厂

        // 异常拦截 ： IthrowsAdvice     环绕拦截（最基本）：IMethodInterceptor
        // 前置拦截： IBeforeAdvice      后置拦截：IAfterReturningAdvice
        private CategoryRepository categoryDAL = new CategoryRepository();

        /// <summary>
        /// 环绕拦截通知
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public object Invoke(IMethodInvocation invocation)
        {
            //对OrderRepository进行方法拦截
            if (invocation.Method.Name == "Save")
            {
                OrderRepository orderDAL = (OrderRepository)invocation.Target;
                return categoryDAL.CheckNo(orderDAL.Order.Category) ? invocation.Proceed() : null;
            }
            else
            {
                return invocation.Proceed();
            }
        }

        /// <summary>
        /// 前置拦截
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <param name="target"></param>
        public void Before(MethodInfo method, object[] args, object target)
        {
            Console.Out.WriteLine("     前置通知： 调用的方法名 : " + method.Name);
            Console.Out.WriteLine("     前置通知： 目标       : " + target);
            Console.Out.WriteLine("     前置通知： 参数为   : ");
            if (args != null)
            {
                foreach (object arg in args)
                {
                    Console.Out.WriteLine("\t: " + arg);
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// 后置拦截通知
        /// </summary>
        /// <param name="returnValue"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <param name="target"></param>
        public void AfterReturning(object returnValue, System.Reflection.MethodInfo method, object[] args, object target)
        {
            if (method.Name=="Save")
            {
                Console.WriteLine("后置拦截成功:");
                Console.WriteLine("目标为："+target);
                Console.WriteLine("参数为:");

                foreach (var item in args)
                {
                    Console.Write(item + "\t");
                }
            }
        }
    }
}
