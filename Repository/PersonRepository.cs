using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class PersonRepository
    {

        private Person person;
        private int no;

        //构造器注入
        public PersonRepository(Person personarg,int no)
        {
            this.person = personarg;
            this.no = no;
        }

        public void GetPerson() 
        {
            Console.WriteLine("构造器注入Person,name:"+person.Name);
        }
    }
}
