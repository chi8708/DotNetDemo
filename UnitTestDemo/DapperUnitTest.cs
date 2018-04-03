using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DapperTest.Service;
using DapperTest.Model;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestDemo
{
    [TestClass]
    public class DapperUnitTest
    {
        int testNum = 1000;
        Table1Service tBLL = new Table1Service();
        [TestMethod]
        public void Add()
        {
            var rCount = 0;
            for (int i = 0; i < testNum; i++)
			{
			     var r= tBLL.Add(new Db_Table1(){
                    Id=i,
                    name="jack"+i
                  });
                 if (r>-1)
                 {
                     rCount += 1;
                 }
			}

            Assert.AreEqual(rCount, 1000);

        }

        [TestMethod]
        public void AddBatch()
        {
            List<Db_Table1> users = new List<Db_Table1>{
                new Db_Table1(){Id=1111,name="rose1"}, 
                new Db_Table1(){Id=1112,name="rose2"}, 
            };

            tBLL.AddBatch(users);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Update()
        {
            Db_Table1 model = new Db_Table1() { Id = 1112, name = "rose1111" };
            Assert.IsTrue(tBLL.Update(model));
            
        }

        [TestMethod]
        public void UpdateBatch()
        {
            List<Db_Table1> users = new List<Db_Table1>{
                new Db_Table1(){Id=1111,name="rose4"}, 
                new Db_Table1(){Id=1112,name="rose5"}, 
            };

            tBLL.UpdateBatch(users);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Delete()
        {
            Assert.IsTrue(tBLL.Delete(1111));
        }

         [TestMethod]
        public void DeleteBatch()
        {
            Assert.IsTrue(tBLL.DeleteBatch(new List<int>() {2,3}));
        }


         [TestMethod]
         public void GetModel()
         {
             Assert.IsTrue(tBLL.GetModel(5).Id==5);
         }

         [TestMethod]
         public void GetModelList1()
         {
             Assert.IsTrue(tBLL.GetModelList(" ID >10  ").Count>1);
         }

         [TestMethod]
         public void GetModelList2()
         {
             Assert.IsTrue(tBLL.GetModelList(" ID >@Id  ", new { Id=10}).Count > 1);
         }


         [TestMethod]
         public void GetDataRecord() 
         {
             Assert.IsTrue(tBLL.GetDataRecord("Id=@Id", new { id = 5 }) == 1);
         }

         [TestMethod]
         public void SelectListByPage()
         {
             Assert.IsTrue(tBLL.SelectListByPage("id>10","id ",1,10).Count()==10);
         }
    }
}
