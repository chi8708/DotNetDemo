using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MvcApi.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /FileUpload/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpFile()
        {
            var file = Request.Files[0];

            var stream = file.InputStream;
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int)stream.Length);
            string bufferText =System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetString(buffer));

            UpFileByApi(buffer, file.FileName.Substring(file.FileName.LastIndexOf(".")));
            return null;
        }

        private string UpFileByApi(byte[] buffer, string ext)
        {
            RestClient client = new RestClient("http://localhost:2843/");

            UploadFile file=new UploadFile(){
           // Context=context,
            Ext=ext,
            ContextByte =buffer
            };
            var result = client.Post(JsonConvert.SerializeObject(file), "FileServer/Upload");

            return  result;
        }

    }
}