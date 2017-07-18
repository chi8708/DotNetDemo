using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;
using Model;

namespace _01SpringIOC
{
    class Program
    {
        static void Main(string[] args)
       {
          IObjectFactory factory= GetIObjectFactory();
          PersonRepository personDAL = factory.GetObject("PersonRepository") as PersonRepository;
          personDAL.GetPerson();
          Person person = factory.GetObject("person") as Person;
          person.Car.Type = "法拉利";
          Console.WriteLine(person.Car.ToString());
          Console.ReadKey();
        }

        public static IObjectFactory GetIObjectFactory()
        {
            string[] xmlFiles = new string[] 
            {
                 "assembly://01SpringIOC/_01SpringIOC/IocObjects.xml"
              // "file://IocObjects.xml"
            };
            IApplicationContext context = new XmlApplicationContext(xmlFiles);
            IObjectFactory factory = (IObjectFactory)context;

            return factory;
        }
    }
}
