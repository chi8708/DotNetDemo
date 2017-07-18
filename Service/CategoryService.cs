using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
   public class CategoryService
   {
       CategoryRepository categoryDAL = new CategoryRepository();
       public bool CheckNo(Category category) 
       {
           return categoryDAL.CheckNo(category);
       }
   }
}
