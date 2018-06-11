using DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GemmyTest.Controllers
{
    public class SqlTestController : Controller
    {

        //
        // GET: /SqlTest/
        public ActionResult Index()
        {
            SqlParameter[] parameter = { 
                                    new SqlParameter ("@OrdersCollection",GetTable()),                                    
                                    };
           var ds= DbHelperSQL.RunProcedure("usp_Orders_Insert", parameter,"tab");
            return View();
        }

        private DataTable GetTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ItemCode", typeof(string));
            table.Columns.Add("UM", typeof(string));
            table.Columns.Add("Quantity", typeof(decimal));
            table.Columns.Add("UnitPrice", typeof(decimal));

            table.Rows.Add("A003-06", "pcs", "10", "3.24");
            table.Rows.Add("A133-26", "pcs", "10", "9.06");
            table.Rows.Add("A605-06", "pcs", "3", "5.67");
            return table;
        }
	}
}