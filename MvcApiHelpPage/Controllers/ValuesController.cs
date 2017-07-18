using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MvcApiHelpPage.Controllers
{
    /// <summary>
    /// ApiHelpPage 测试
    /// </summary>
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        
        /// <summary>
        /// Get 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
           
            return "value";
        }

        // POST api/values
        public void Post([FromBody]dynamic value)
        {
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
        /// 测试关联获取
        /// </summary>
        /// <param name="id">主Id</param>
        /// <param name="otherId">次Id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetValuesWithOther/{id}/other/{otherId}")]
        public string GetValuesWithOther(int id,int otherId) 
        {

            return otherId.ToString();
        }

     
    }

  
}
