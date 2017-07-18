using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Aspect
{
    public class BeforeAdviseOrder : IBeforeAdvice //该接口没有Before方法 所以前置拦截有问题
    {
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
    }
}
