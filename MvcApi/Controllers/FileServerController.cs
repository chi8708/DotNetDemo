using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MvcApi.Controllers
{
    public class FileServerController : ApiController
    {
        [Route("FileServer/Upload")]
        public string Upload(UploadFile file)
        {
          //  var context = file.Context;
            var ext = file.Ext;
        //    var buffer=System.Text.Encoding.UTF8.GetBytes(System.Web.HttpUtility.UrlDecode(context.ToString()));

            var root = HttpContext.Current.Server.MapPath("~/upload");
            var path =string.Format("{0}\\{1}{2}",root, Guid.NewGuid(),ext);

            File.WriteAllBytes(path,file.ContextByte);
            return "ok";
        }

        /// <summary>
        /// 上传用户头像（只能传jpg图
        /// </summary>
        /// <param name="EID"></param>
        /// <returns></returns>
        [Route("Posthead")]
        [HttpPost]
        public int Posthead(int EID = 0)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/userhead");//指定要将文件存入的服务器物理位置  
            //var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                var sp = new MultipartMemoryStreamProvider();
                Task.Run(async () => await Request.Content.ReadAsMultipartAsync(sp)).Wait();
                if (sp.Contents.Count > 0)
                {
                    foreach (var item in sp.Contents)
                    {
                        if (item.Headers.ContentDisposition.FileName != null)
                        {
                            string filename = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                            //int dianindex = filename.LastIndexOf(".");
                            string aLastName = filename.Substring(filename.LastIndexOf(".") + 1, (filename.Length - filename.LastIndexOf(".") - 1)); //扩展名

                            var newFileName = root + "\\" + EID.ToString() + "." + aLastName;
                            var ms = item.ReadAsStreamAsync().Result;
                            using (var br = new BinaryReader(ms))
                            {
                                var data = br.ReadBytes((int)ms.Length);
                                File.WriteAllBytes(newFileName, data);
                            }
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return -1;
            }
            return 1;
        }

    }
}
