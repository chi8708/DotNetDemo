using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApiHelpPage.Controllers
{

   /// <summary>
   /// People Api
   /// </summary>
    [RoutePrefix("api/People")]
    public class PeopleController : ApiController
    {
        /// <summary>
        /// 获取所有People
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public List<People> GetPeople()
        {
            return new List<People>();
        }

        /// <summary>
        /// 添加People
        /// </summary>
        /// <param name="people">要添加的People</param>
        [HttpPost]
        public List<People> PostPeople([FromBody]List<People> people)
        {
            var data = JsonConvert.SerializeObject(new List<People>() { new People() { Age = 12, Name = "ddd" } });
            return people;
        }
    }

    /// <summary>
    ///People 
    /// </summary>
    public class People
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }
}
