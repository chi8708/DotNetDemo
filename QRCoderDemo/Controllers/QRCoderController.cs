using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QRCoderDemo.Controllers
{
    public class QRCoderController : Controller
    {
        //
        // GET: /QRCoder/
        public ActionResult Index()
        {
            return View();
        }


        public void Gener(string text) 
        {
            //QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.M);
            //QRCode qrCode = new QRCode(qrCodeData);
            //Bitmap qrCodeImage = qrCode.GetGraphic(8, Color.Black, Color.White, icon: ((Bitmap)Bitmap.FromFile(Server.MapPath("~/logo.jpg"))));

            QRCoder.PayloadGenerator.OneTimePassword generator = new QRCoder.PayloadGenerator.OneTimePassword()
            {
                Secret = "pwq6 5q55",
                Issuer = "Google",
                Label = "test@google.com",
            };
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(8);
            Response.ClearContent();
            qrCodeImage.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg); ;
        }




	}
}