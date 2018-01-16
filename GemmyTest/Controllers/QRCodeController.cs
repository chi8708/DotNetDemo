using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using System.IO;
using System.Drawing;
using System.Threading;

namespace GemmyTest.Controllers
{
    public class QRCodeController : Controller
    {
        //
        // GET: /QRCode/
        public ActionResult Index()
        {
            var r = Request["r"];

            if (r == "1")
            {
                Thread.Sleep(10000);
            }
            return View();
        }

        public ActionResult Create()
        {
            var payCodeEN = "http://www.baidu.com";
            using (MemoryStream ms = new MemoryStream())
            {
                var img = QRCoderHelper.Create(payCodeEN);

                var comImg = QRCoderHelper.CombinImage2(Image.FromFile(Server.MapPath("../image/Desert.jpg")), img);
                comImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                comImg.Dispose();
                img.Dispose();
                

                return File(ms.ToArray(), "image/jpeg");
            }
        }
    }
}