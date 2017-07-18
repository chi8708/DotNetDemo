using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CategoryRepository
    {
        public bool CheckNo(Category category)
        {
            if (category.No > 0)
            {
                Console.WriteLine("数据层  检查类别成功");
                return true;
            }
            return false;
        }
    }
}
