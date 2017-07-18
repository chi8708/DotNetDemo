using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsoseTest.Models
{

// 有一个私有的无参构造函数，这可以防止其他类实例化它，而且单例类也不应该被继承，
   //如果单例类允许继承那么每个子类都可以创建实例，这就违背了Singleton模式“唯一实例”的初衷。
//单例类被定义为sealed,就像前面提到的该类不应该被继承，所以为了保险起见可以把该类定义成不允许派生，
   //但没有要求一定要这样定义。
//一个静态的变量用来保存单实例的引用。
//一个公有的静态方法用来获取单实例的引用，如果实例为null即创建一个。
    
    //1.双重锁 懒汉模式 性能不咋地
    public  sealed partial class Signal
    {
        private static Signal signal;
        private static object o=new object();


        private string test = "test2";
        public string Test { get { return test; } set { this.test = value; } }

        private Signal() { }

        public static Signal SignalInstance 
        {
            get 
            {

                if (null==signal)
                {
                    lock (o)
                    {
                        if (null==signal)
                        {
                            signal= new Signal();
                        }
                    }
                }

                return signal;
            
            }
        }

    }

    //2. 定义静态类 延迟初始化
    public sealed partial class Signal 
    {

        public static Signal SignalInstance2
        {
            get { return Nest.signal2; }
        }
        private class Nest 
        {
            private Nest() {  }

            internal static Signal signal2 = new Signal();
        }
    }

    //3.Lazy<T> type 性能最好

    public sealed partial class Signal
    {
        private static Lazy<Signal> ls = new Lazy<Signal>(()=>new Signal());
        public static Signal SignalInstance3
        {
            get { return ls.Value; }
        }
    }



    public class People 
    {
        //静态字段只会调用一次
        public static string str = "staticstr";
    }


}