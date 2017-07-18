using DapperTest.Dapper.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Security.Cryptography;

namespace DapperTest.Controllers
{
    public class DapperTestController : Controller
    {
        private  static readonly string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        IDbConnection conn = new SqlConnection(connStr);
        // GET: DapperTest
        public ActionResult Index()
        {
            bool result = false;

            IList<User> users = GetAll();
           // users = GetAllUserWithModules();
            //Add(new User{Loginno="Dapper",Password="123456"});

            //User user = GetSingleById(37);
            //user.Loginno = "DapperModified1";
            //Update(user);

            //result = Delete(48);

           IList<UserModule>  modules= GetAllModuleWithUser();
          // DeleteUserWithModule(35);
          // AdddChatRecord(new ChatRecord() { Fromloginno = "dapper", Tologinno = "newdapper", Message = "dapper procedure", Sendtime = DateTime.Now, Type = 1, Status = 1 });
           return View();
        }

        private IList<User> GetAll() 
        {
            string sql = "select * from [user]";
 
            return conn.Query<User>(sql).ToList();
        }

        private User GetSingleById(int id) 
        {
            string sql = "select * from [user] where id=@id";

            return conn.Query<User>(sql, param: new {id=id}).FirstOrDefault();
            //param 必须是对象项属性 param:id 不正确
        }

        private User Add(User user) 
        {
            string sql = "insert into [user](loginNo,password) OUTPUT  inserted.Id values(@loginNo,@password)";
            user.Id=(int)conn.ExecuteScalar(sql, user);

            return user;
        }
        private bool Update(User user) 
        {
            string sql = "update [user] set loginNo=@loginNo where id=@id";
  
            return conn.Execute(sql, user)>=1;
        }

        private bool Delete(int id) 
        {
            string sql = "delete from [user] where id=@id";

            return conn.Execute(sql, new { id = id })>=1;
        }

        //1-N
        private IList<User> GetAllUserWithModules() 
        {
            string sql = "select * from [user] as a left join User_Module as b on a.Id=b.UserId";

            //保证join UserModule条数
            User user = null;

            //异常  When using the multi-mapping APIs ensure you set the splitOn param if you have keys other than Id
            //必须添加 splitOn 同时查询字段必须包括外键 b.UserId 
            return conn.Query<User,UserModule, User>(sql, (u, m) => {

                if (user==null||user.Id!=u.Id)
                {
                    user = u;
                }

                if (m != null)
                {
                    if (user.Modules==null)
                    {
                        user.Modules = new List<UserModule>();
                    }
                    user.Modules.Add(m);
                }

                return user;
            },splitOn:"Id,UserId").Distinct().ToList();

        }

        //1*1
        private IList<UserModule> GetAllModuleWithUser() 
        {
            //inner join
            string sql = "select * from User_Module a left join [User] b on a.UserId=b.Id ";

            return conn.Query<UserModule, User, UserModule>(sql,(m, u) =>
            {
                m.User = u;
                return m;

            },"UserId,Id").ToList();
        }

        //事务
        private bool DeleteUserWithModule(int id) 
        {
            string sql1 = "delete from [User] where id=@id";
            string sql2 = "delete from User_Module where userId=@userId";

            using (conn)
            {
                conn.Open();
   
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        //Query必须添加 transaction:transaction
                        conn.Execute(sql1, new { id = id},transaction:transaction);
                        conn.Execute(sql2, new { userId = id },transaction:transaction);
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();

                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 存储过程
        /// </summary>
        /// <param name="chatRecord"></param>
        /// <returns></returns>
        private bool AdddChatRecord(ChatRecord chatRecord) 
        {
            conn.Execute("usp_insertChatRecord", param: chatRecord,commandType:System.Data.CommandType.StoredProcedure);

            return true;
        }


    }
}