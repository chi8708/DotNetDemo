using DapperTest.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperTest.Model;
using DapperExtensions;

namespace UnitTestDemo
{
   [TestClass]
   public class Tabl2Test
    {
      
        int testNum = 1000;
        Tabl2Service tBLL = new Tabl2Service();
        [TestMethod]
        #region DapperT4
        public void Add()
        {
            var rCount = 0;
            for (int i = 0; i < testNum; i++)
            {
                var r = tBLL.Add(new Db_Tabl2()
                {
                    id = i,
                    Name="jack"+i
                });
                if (r > -1)
                {
                    rCount += 1;
                }
            }

            Assert.AreEqual(rCount, 1000);

        }

        [TestMethod]
        public void AddBatch()
        {
            List<Db_Tabl2> users = new List<Db_Tabl2>{
                new Db_Tabl2(){id=1111,Name="rose1"}, 
                new Db_Tabl2(){id=1112,Name="rose2"}, 
            };

            tBLL.AddBatch(users);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Update()
        {
            Db_Tabl2 model = new Db_Tabl2() { id = 2005, Name = "rose1111" };
            Assert.IsTrue(tBLL.Update(model));

        }

        [TestMethod]
        public void UpdateBatch()
        {
            List<Db_Tabl2> users = new List<Db_Tabl2>{
                new Db_Tabl2(){id=2005,Name="rose4"}, 
                new Db_Tabl2(){id=2006,Name="rose5"}, 
            };

            tBLL.UpdateBatch(users);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Delete()
        {
            Assert.IsTrue(tBLL.Delete(2005));
        }

        [TestMethod]
        public void DeleteBatch()
        {
            Assert.IsTrue(tBLL.DeleteBatch(new List<int>() { 2007, 2008 }));
        }


        [TestMethod]
        public void GetModel()
        {
            Assert.IsTrue(tBLL.GetModel(2006).id == 2006);
        }

        [TestMethod]
        public void GetModelList1()
        {
            Assert.IsTrue(tBLL.GetModelList(" ID >10  ").Count > 1);
        }

        [TestMethod]
        public void GetModelList2()
        {
            Assert.IsTrue(tBLL.GetModelList(" ID >@id  ", new { id = 10 }).Count > 1);
        }


        [TestMethod]
        public void GetDataRecord()
        {
            Assert.IsTrue(tBLL.GetDataRecord("id=@id", new { id = 2006 }) == 1);
        }

        [TestMethod]
        public void SelectListByPage()
        {
            Assert.IsTrue(tBLL.SelectListByPage("id>10", "id ", 1, 10).Count() == 10);
        } 
        #endregion

       # region DapperExtensions
       [TestMethod]
        public void Insert()
        {
            var rCount = 0;
            for (int i = 0; i < testNum; i++)
            {
                var r = tBLL.Insert(new Tabl2()
                {
                    id = i,
                    Name="rose"+i
                });
                if (r > -1)
                {
                    rCount += 1;
                }
            }

            Assert.AreEqual(rCount, 1000);

        }

       [TestMethod]
       public void InsertBatch()
       {
           List<Tabl2> users = new List<Tabl2>{
                new Tabl2(){id=1111,Name="rose1"}, 
                new Tabl2(){id=1112,Name="rose2"}, 
            };

           tBLL.InsertBatch(users);
           Assert.IsTrue(true);

       }

       [TestMethod]
       public void UpdateExt()
       {
           var model = new Tabl2() { id = 14017, Name = "rose1eee", AId = 2222 };

           tBLL.Update(model);
           Assert.IsTrue(true);

       }


       [TestMethod]
       public void UpdateBatchExt()
       {
           List<Tabl2> users = new List<Tabl2>{
                new Tabl2(){id=14017,Name="rose4"}, 
                new Tabl2(){id=14018,Name="rose5"}, 
            };

           tBLL.UpdateBatch(users);
           Assert.IsTrue(true);
       }



       [TestMethod]
       public void DeleteExt()
       {
           Assert.IsTrue(tBLL.Delete(14017));
       }

       [TestMethod]
       public void DeleteBatchExt()
       {
           List<Tabl2> users = new List<Tabl2>{
                new Tabl2(){id=14019,Name="rose4"}, 
                new Tabl2(){id=14020,Name="rose5"}, 
            };

           Assert.IsTrue(tBLL.DeleteBatch(users));
       }


       [TestMethod]
       public void GetModelExt()
       {
           Assert.IsTrue(tBLL.Get(14023).id == 14023);
       }

       [TestMethod]
       public void GetModelList1Ext()
       {
           var cond = Predicates.Field<Tabl2>(p => p.id, Operator.Ge, 14023);
           Assert.IsTrue(tBLL.GetList(cond).Count() > 1);
       }


       [TestMethod]
       public void GetDataRecordExt()
       {
           var cond = Predicates.Field<Tabl2>(p => p.id, Operator.Eq, 14023);
           Assert.IsTrue(tBLL.Count(cond) == 1);
       }

       [TestMethod]
       public void SelectListByPageExt()
       {
           var g = new PredicateGroup() { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
           g.Predicates.Add(Predicates.Field<Tabl2>(p => p.id, Operator.Gt, 14023));
           g.Predicates.Add(Predicates.Field<Tabl2>(p=>p.Name,Operator.Like,"%ose2%"));
           var sort=new List<ISort>(){new Sort(){PropertyName="id"}};

           Assert.IsTrue(tBLL.GetPage(g,sort,1,10).Count() == 10);
       } 
       # endregion

    }
}
