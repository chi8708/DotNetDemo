using DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.EnterpriseServices;
using Dapper;

namespace GemmyTest.Controllers
{
        
    public class EnterpriseServicesTestController : Controller
    {
     
        // GET: /EnterpriseServicesTest/
        public ActionResult Index()
        {
            new AddService().AddTran();
            return View();
        }

      
	}

    [System.Runtime.InteropServices.ComVisible(true)]
    [Transaction(TransactionOption.Required)]
    public class AddService : ServicedComponent 
    {
        private static readonly string connStr = PubConstant.ConnectionString;
        IDbConnection conn = new SqlConnection(connStr);

        public AddService() { }

        [AutoComplete]
        public void AddTran() 
        {
            InsertTestTable1();
            var a = 0;
            var b = 1/a;
        }
        
       
        public void InsertTestTable1()
        {
            var sql = @"INSERT INTO [Table1]([name]) VALUES (@a)";
            sql = string.Format(sql, new[] { new { a = "es1" }, new { a = "es2" } });

            conn.Execute(sql);
        }
    }
}