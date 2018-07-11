using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiDoc.Controllers
{

    //
    /// <summary>
    /// 测试接口
    /// </summary>
    /// <remarks>测试2</remarks>
    ///<returns></returns>
    public class ValuesController : ApiController
    {
        List<People> data = new List<People> { new People { Id = 1, Name = "test1", Age = 11 }, new People { Id = 2, Name = "test2", Age = 13 } };
        // GET api/values
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IEnumerable<People> Get()
        {
            return data;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        
        // POST api/values
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">用户</param>
        /// <returns></returns>
        public List<People> Post(People model)
        {
           data.Add(model);
            return data;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

       
        /// <summary>
        /// 测试人
        /// </summary>
       public class People
       {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
       /// <summary>
       /// 性别
       /// </summary>
        public string Gender { get; set; }
       }
    }
}
