using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApi.Controllers
{
    public class BaseApiController : ApiController
    {
        public override System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Web.Http.Controllers.HttpControllerContext controllerContext, System.Threading.CancellationToken cancellationToken)
        {
            controllerContext.Configuration.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    
    }
}
