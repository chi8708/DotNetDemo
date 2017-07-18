using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aspect
{
    public class AfterAdviseOrder : IAfterReturningAdvice
    {
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
