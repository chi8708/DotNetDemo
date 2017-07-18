using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using System.Web.Http;
using System.Net;
using MvcApi.Models;
using System.Net.Http.Formatting;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web.Http.WebHost;
using System.Web.Http.Description;
using System.Security.Claims;
using MvcApi.App_Start;
using System.Collections;

namespace MvcApi.Controllers
{
    /// <summary>
    /// 测试Api
    /// </summary>
    public class TestApiController : ApiController
    {
        
        //
         IList<People> data = new List<People> { new People {Id=1, Name = "test1", Age = 11 }, new People {Id=2, Name = "test2", Age = 13 } };

        /// <summary>
         /// Get api/TestApi
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/TestApi")]
        public IEnumerable<People> GetAll()
        {            
           var type= (User.Identity).AuthenticationType;
            var userData=((System.Security.Claims.ClaimsIdentity)(User.Identity)).
                Claims.First(p => p.Type==ClaimTypes.UserData).Value;
            People model = JsonConvert.DeserializeObject<People>(userData);

            return data;
        }

        /// <summary>
        /// 测试分页api
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orderBy"></param>
        /// <param name="age"></param>
        /// <param name="isAsc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public object GetPage([FromUri]string name,[FromUri]string orderBy, int? age,bool isAsc=true,int pageIndex=1,int pageSize=2) 
        {
             data.Add(new People {Id=4, Name = "test4", Age = 22 });
             data=data.Where(p => (string.IsNullOrEmpty(name) || p.Name.Contains(name))
                &&(!age.HasValue||p.Age.Equals(age))
                ).ToList();

            var count=data.Count();
            var totalPage =(int)Math.Ceiling(count / (double)pageSize);

            data= data.Sort(orderBy, isAsc).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return new { TotalPage = totalPage,Data=data };

        }

        public string PutUser([FromBody]People user) 
        {
           var model= data.FirstOrDefault(p => p.Id == user.Id);
           model = user;

           return "ok";
        }


        /// <summary>
        /// 只会返回该方法所序列化的data
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public HttpResponseMessage GetAllContacts(string callback)
        {
            HttpConfiguration config = new HttpConfiguration();
            data.Add(new People { Name = "test3", Age = 22 });
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string content = string.Format("{0}({1})", callback, serializer.Serialize(data));
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content, Encoding.UTF8, "text/javascript")
            };

        }

        //如果是web api 不是mvc 就要添加该方法 解决jsonp、
        public string Options()
        {

            return null; // HTTP 200 response with empty body

        }
    }


    public static class Extensions
    {
        public static IEnumerable<TSource> Sort<TSource>(this IEnumerable<TSource> source, string propertyName, bool isAsc)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            var sourceType = typeof(TSource);
            var propertyInfo = sourceType.GetProperty(propertyName);

            var parameterExpression = Expression.Parameter(sourceType);
            var bodyExpression = Expression.Property(parameterExpression, propertyInfo);
            var selecterExpression = Expression.Lambda(bodyExpression, parameterExpression);

            var methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var methodExpression = Expression.Call(typeof(Enumerable), methodName, new Type[] { sourceType, propertyInfo.PropertyType }, Expression.Constant(source), selecterExpression);
            return (IOrderedEnumerable<TSource>)methodExpression.Method.Invoke(null, new object[] { source, selecterExpression.Compile() });
        }
    }
}
