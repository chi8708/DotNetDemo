using GemmyTest.RedisUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GemmyTest.Controllers
{
    public class RedisTestController : Controller
    {
        //
        // GET: /RedisTest/
        public ActionResult Index()
        {
            string key = "zlh";
            //清空数据库
            DoRedisBase.Core.FlushAll();
            //给list赋值
            DoRedisBase.Core.PushItemToList(key, "1");
            DoRedisBase.Core.PushItemToList(key, "2");
            DoRedisBase.Core.AddItemToList(key, "3");
            DoRedisBase.Core.PrependItemToList(key, "0");
            DoRedisBase.Core.AddRangeToList(key, new List<string>() { "4", "5", "6" });
            #region 阻塞
            //启用一个线程来处理阻塞的数据集合
          //  new Thread(new ThreadStart(RunBlock)).Start();
            var list=  new DoRedisList().Get(key);
            #endregion
         //   Console.ReadKey();
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        public static void RunBlock()
        {
            while (true)
            {
                //如果key为zlh的list集合中有数据，则读出，如果没有则等待2个小时，2个小时中只要有数据进入这里就可以给打印出来，类似一个简易的消息队列功能。
                Console.WriteLine(DoRedisBase.Core.BlockingPopItemFromList("zlh", TimeSpan.FromHours(2)));
            }
        }

        public ActionResult Tran() 
        {
            //清空数据库
            DoRedisBase.Core.FlushAll();
            //声明事务
            using (var tran = RedisManager.GetClient().CreateTransaction())
            {
                try
                {
                    tran.QueueCommand(p =>
                    {
                        //操作redis数据命令
                        DoRedisBase.Core.Set<int>("name", 30);
                        long i = DoRedisBase.Core.IncrementValueBy("name", 1);
                    });
                    //提交事务
                    tran.Commit();
                }
                catch
                {
                    //回滚事务
                    tran.Rollback();
                }
                ////操作redis数据命令
                //RedisManager.GetClient().Set<int>("zlh", 30);
                ////声明锁，网页程序可获得锁效果
                //using (RedisManager.GetClient().AcquireLock("zlh"))
                //{
                //    RedisManager.GetClient().Set<int>("zlh", 31);
                //    Thread.Sleep(10000);
                //}
            }

            return null;
        }

        public ActionResult SessionTest() 
        {
            Session["Test"] = "1111";
            return View();
        }
	}
}