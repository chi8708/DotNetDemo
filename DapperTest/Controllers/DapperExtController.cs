using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using DapperExtensions;
using DapperTest.Model;
using Newtonsoft.Json;

namespace DapperTest.Controllers
{
    public class DapperExtController : Controller
    {

        //
        // GET: /DapperExt/
        public ActionResult Get()
        {
            Table1 model=new Table1();
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                 model = cn.Get<Table1>(1);
            }
            return Content(model.name);
        }

        public ActionResult Add() 
        {
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                Tabl2 model = new Tabl2() { AId = 1111, Name = "test" };
                 Table4 model2 = new Table4() { Code="ssdb", Name = "test" };
                var r = cn.Insert(model);
                cn.Insert(model2);
                return Content(JsonConvert.SerializeObject(r));
            }
           
        }


        public ActionResult AddBatch()
        {
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                Tabl2 model = new Tabl2() { AId = 1111, Name = "test" };
                Tabl2 model2 = new Tabl2() { AId = 1112, Name = "test2" };
                var r = cn.Insert(model);
                cn.Insert(model2);
                return null;
            }

        }

        public ActionResult Update()
        {

            Tabl2 model = new Tabl2() { id=7007, AId = 1111, Name = "test2222" };
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                var r = cn.Update(model);
                return Content(r.ToString());
            }

        }


        public ActionResult Delete()
        {


            Table4 model = new Table4() { Code = "ssdb" };
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                var r = cn.Delete(model);
                return Content(r.ToString());
            }

        }

        public ActionResult GetList()
        {

            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                var pg = new PredicateGroup { Operator = GroupOperator.And,Predicates = new List<IPredicate>() };
                pg.Predicates.Add(Predicates.Field<Table1>(f => f.Id, Operator.Ge, 10));
                pg.Predicates.Add(Predicates.Field<Table1>(f => f.name, Operator.Like, "jack1%"));

                var r = cn.GetList<Table1>(pg);
                return Content(JsonConvert.SerializeObject(r));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPageList()
        {

            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pg.Predicates.Add(Predicates.Field<Table1>(f => f.Id, Operator.Ge, 10));
                pg.Predicates.Add(Predicates.Field<Table1>(f => f.name, Operator.Like, "jack1%"));

                IList<ISort> sort=new List<ISort>(){(new Sort() { PropertyName = "id" })};
                var r = cn.GetPage<Table1>(pg,sort, 1, 20);
                return Content(JsonConvert.SerializeObject(r));
            }

        }

        public partial class Table1
        {


            /// <summary>
            /// 
            /// </summary>		
            public int Id { get; set; }


            /// <summary>
            /// 
            /// </summary>		
            public string name { get; set; }

        }

        public partial class Tabl2
        {


            /// <summary>
            /// 
            /// </summary>		
            public int id { get; set; }


            /// <summary>
            /// 
            /// </summary>		
            public int? AId { get; set; }


            /// <summary>
            /// 
            /// </summary>		
            public string Name { get; set; }

        }

        public partial class Table4
        {
            public string Code { get; set; }

            public string  Name { get; set; }

        }
	}
}