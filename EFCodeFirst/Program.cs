using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCodeFirst.Entity;

namespace EFCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db=new ShopContext())
            {
                var model = new NewsItem()
                {
                    Title="new1",
                    Short="short2",
                    Full="哈哈哈个",
                    MetaKeywords="ggg"
                };
                db.Set<NewsItem>().Add(model);
                db.SaveChanges();
            }
        }
    }
}
