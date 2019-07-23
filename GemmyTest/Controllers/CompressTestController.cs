using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GemmyTest.Controllers
{
    public class CompressTestController : Controller
    {
        // GET: CompressTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Compress()
        {
            var outFile = Server.MapPath("~/compress");
            SevenZipCompressor.Zip(new List<string> { @"E:\compressTest" }, outFile);
            return View();
        }

        [HttpPost]
        public void Download(string o, string t)
        {
            string filePath = @"D:\Download\排期（20190412）.xlsx";

            #region 处理下文件
            var arr = filePath.Split('\\');
            string fileName = arr[arr.Length - 1];
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 102400;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];

                Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                while (dataLengthToRead > 0 && Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    Response.OutputStream.Write(buffer, 0, lengthRead);
                    Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                Response.Close();
                Response.End();
            }
            #endregion
        }
    }
}