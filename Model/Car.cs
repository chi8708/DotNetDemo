using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Car
    {
        public string Type { get; set; }
        public string Wheel { get; set; }

        public float Speed { get; set; }

        public void Run()
        {
            Console.WriteLine("车在跑！");
        }
        public override string ToString()
        {
            return "Car11"+this.Type;
        }
    }
}
