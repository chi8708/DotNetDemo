using Common;
using Newtonsoft.Json;
using RedisHelp;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GemmyTest.Controllers
{
    public class SETestController : Controller
    {
        RedisHelper redis = new RedisHelper(1);
        //
        // GET: /SETest/
        public ActionResult Index()
        {

            //并发
            //Thread th1 = new Thread(ConcurrentTest);
            //Thread th2 = new Thread(ConcurrentTest);
            //th1.Start();
            //th2.Start();
           // System.Threading.Tasks.Parallel.Invoke(ConcurrentTest, ConcurrentTest);
            //Task.Run(()=>ConcurrentTest());
            //Task.Run(() => ConcurrentTest());
            //return Content("ok");

            #region String

            string str = "123";
            Demo demo = new Demo()
            {
                Id = 1,
                Name = "123"
            };

            var resultkey1 = redis.StringSet("key1", "ddd老师");
            var str2 = redis.StringGet("key1");
            var resukt = redis.StringSet("redis_string_test", str);
            var str1 = redis.StringGet("redis_string_test");
            redis.StringSet("redis_string_model", demo);
            var model = redis.StringGet<Demo>("redis_string_model");

            for (int i = 0; i < 10; i++)
            {
                redis.StringIncrement("StringIncrement", 2);
            }
            for (int i = 0; i < 10; i++)
            {
                redis.StringDecrement("StringIncrement");
            }
            redis.StringSet("redis_string_model1", demo, TimeSpan.FromSeconds(10));

            #endregion String

            #region List

            for (int i = 0; i < 10; i++)
            {
                redis.ListRightPush("list", i);
            }

            for (int i = 10; i < 20; i++)
            {
                redis.ListLeftPush("list", i);
            }
            var length = redis.ListLength("list");

            var leftpop = redis.ListLeftPop<string>("list");
            var rightPop = redis.ListRightPop<string>("list");

            var list = redis.ListRange<int>("list");

            #endregion List

            #region Hash

            redis.HashSet("user", "u1", "123");
            redis.HashSet("user", "u2", "1234");
            redis.HashSet("user", "u3", "1235");
            var news = redis.HashGet<string>("user", "u2");
            
            //hash批量
            var users = new List<Demo>(){
                     new Demo{Id=111,Name="jack"},
                     new Demo{Id=222,Name="rose"},
                     };

            List<HashEntry> hes=new  List<HashEntry>();
            foreach (var item in users)
            {
                hes.Add(new HashEntry(item.Id,JsonConvert.SerializeObject(item)));
            }
            redis.HashSet("hashBatch", hes);

            #endregion Hash

            #region 发布订阅

            redis.Subscribe("Channel1");
            for (int i = 0; i < 10; i++)
            {
                redis.Publish("Channel1", "msg" + i);
                if (i == 2)
                {
                    redis.Unsubscribe("Channel1");
                }
            }

            #endregion 发布订阅

            #region 事务

            var tran = redis.CreateTransaction();

            tran.StringSetAsync("tran_string", "test1");
            tran.StringSetAsync("tran_string1", "test2");
            bool committed = tran.Execute();

            #endregion 事务

            #region Lock

            var db = redis.GetDatabase();
            RedisValue token = Environment.MachineName;
            if (db.LockTake("ConcurrentKey1", token, TimeSpan.FromSeconds(10)))
            {
                try
                {
                    //TODO:开始做你需要的事情
                    //并发
                    Thread th1 = new Thread(ConcurrentTest);
                    Thread th2 = new Thread(ConcurrentTest);
                    th1.Start();
                    th2.Start();
                    Thread.Sleep(5000);
                }
                finally
                {
                    db.LockRelease("ConcurrentKey1", token);
                }
            }

            return Content("ok");
            #endregion Lock
        }

       static object o = new object();
        public void ConcurrentTest() 
        {
        //    redis.KeyDelete("ConcurrentKey");
            //加锁防止并发
            lock (o) 
            {
                for (int i = 0; i < 5000; i++)
                {
                    int result = Convert.ToInt32(redis.StringGet("ConcurrentKey"));
                    redis.StringSet("ConcurrentKey", result + 1);
                }
            }

        }
        public class Demo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
	}
}