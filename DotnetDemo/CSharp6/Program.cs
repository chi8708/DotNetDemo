using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;//6.0可引入静态类
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace CSharp6
{
    //C#6.0
    class Program
    {
        static void Main(string[] args)
        {
            Point p = new Point();

            Console.WriteLine($"point Dist:{p.Dist}");
            Console.WriteLine($"point tostring:{p.ToString()}");
            Console.WriteLine($"point ToJson:{p.ToJson()}");
            Console.WriteLine($"point FromJson:{p.FromJson(p.ToJson())}");
            Console.ReadKey();
        }

    }

    public class Point
    {
        //属性默认值
        public int X { get; set; } = 8;

        public int Y { get; set; }
        //public double Dist
        //{
        //    get { return Sqrt(X * X + Y * Y); }
        //}

         //lambda属性
        public double Dist =>Sqrt(X* X + Y* Y);

        //lambda方法
        public override string ToString() => $"({X},{Y})";

        //带索引的对象初始化器Index initializers 
        public JObject ToJson() => new JObject() { ["x"] = X, ["y"] = Y };


        //空值判断Null
        public Point FromJson(JObject json)
        {
            //if (json!=null&&json["x"]?.Type==JTokenType.Integer)
            //{

            //}
            
            if (json?["x"]?.Type==JTokenType.Integer&& 
                json?["y"]?.Type == JTokenType.Integer)
            {
                return new Point() { X = (int)json["x"], Y =(int)json["y"] }; 
            }

            return null;
        }
    }
}
