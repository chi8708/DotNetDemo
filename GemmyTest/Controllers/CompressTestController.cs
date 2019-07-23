using System;
using System.Collections.Generic;
using System.IO;
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
            var outFile = Server.MapPath("~/compress/a.zip");
            //var resetDir = @"E:\compressTest\a";
            //if (Directory.Exists(resetDir))
            //{
            //    Directory.Delete(resetDir);
            //}
            //Directory.CreateDirectory(resetDir);

            //CopyFile(@"E:\compressTest",resetDir);
            SevenZipCompressor.Zip(new List<string> { @"E:\compressTest\" }, outFile);
            return Content(outFile);
        }

        public ActionResult CompressStream()
        {
            var outFile = Server.MapPath("~/compress/a.zip");
            var stream= SevenZipCompressor.ZipStream(new List<string> { @"E:\compressTest" }, outFile);
            //return new FileStreamResult(stream, "application/octet-stream");

            return File(outFile, "application/octet-stream");
        }

        public void CopyFile(string inDir,string outDir)
        {
            var files = Directory.GetFiles(inDir);
         
            foreach (var file in files)
            {
                System.IO.File.Copy(file, outDir + "\\O" + file.Substring(file.LastIndexOf('\\')+2),true);
            }
        }
    }
}