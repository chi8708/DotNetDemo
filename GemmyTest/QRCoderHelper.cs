using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 二维码帮助类
    /// </summary>
    public class QRCoderHelper
    {
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="text">二维码数据</param>
        /// <param name="logoPath">logo地址</param>
        /// <param name="iconSizePercent">logo大小</param>
        /// <param name="drawQuietZones">是否添加白色边框</param>
        /// <returns></returns>
        public static Bitmap Create(string text, string logoPath = "",int iconSizePercent=25,bool drawQuietZones=false)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.M);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage;
            if (string.IsNullOrEmpty(logoPath))
            {
                qrCodeImage = qrCode.GetGraphic(8, Color.Black, Color.White,drawQuietZones: drawQuietZones);
            }
            else
            {
                //qrCodeImage = qrCode.GetGraphic(8, Color.Black, Color.White, drawQuietZones: false);
                //qrCodeImage = CombinImage(qrCodeImage, logoPath) as Bitmap;
                if (logoPath.Trim().Substring(0, 4).ToLower() == "http")
                {
                    qrCodeImage = qrCode.GetGraphic(8, Color.Black, Color.White, icon: Get_img(logoPath), iconSizePercent: iconSizePercent, drawQuietZones: drawQuietZones);
                }
                else
                {
                    qrCodeImage = qrCode.GetGraphic(8, Color.Black, Color.White, icon: ((Bitmap)Bitmap.FromFile(logoPath)), iconSizePercent: iconSizePercent, drawQuietZones: drawQuietZones);
                }
                
                
            }
            return qrCodeImage;
            //Response.ClearContent();
            //qrCodeImage.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg); ;

        }

       

        /// <summary>    
        /// 调用此函数后使此两种图片合并，类似相册，有个    
        /// 背景图，中间贴自己的目标图片    
        /// </summary>    
        /// <param name="imgBack">粘贴的源图片</param>    
        /// <param name="destImg">粘贴的目标图片</param>    
        public static Image CombinImage(Image imgBack, string destImg)
        {
            Image img = Image.FromFile(destImg);        //照片图片      
            if (img.Height != 65 || img.Width != 65)
            {
                img = KiResizeImage(img, 65, 65, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框    

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }


        /// <summary>    
        /// 调用此函数后使此两种图片合并，类似相册，有个    
        /// 背景图，中间贴自己的目标图片    
        /// </summary>    
        /// <param name="imgBack">粘贴的源图片</param>    
        /// <param name="destImg">粘贴的目标图片</param>    
        public static Image CombinImage2(Image imgBack, Image destImg)
        {
            //Image img = Image.FromFile(destImg);        //照片图片     
            Image img = destImg;
            if (img.Height != 180 || img.Width != 180)
            {
                img = KiResizeImage(img, 180, 180, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框    

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    

            g.DrawImage(img,10,20, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }

        public static Bitmap Get_img(string url)
        {
            Bitmap img = null;
            HttpWebRequest req;
            HttpWebResponse res = null;
            try
            {
                System.Uri httpUrl = new System.Uri(url);
                req = (HttpWebRequest)(WebRequest.Create(httpUrl));
                req.Timeout = 180000; //设置超时值10秒
                req.UserAgent = "XXXXX";
                req.Accept = "XXXXXX";
                req.Method = "GET";
                res = (HttpWebResponse)(req.GetResponse());
                img = new Bitmap(res.GetResponseStream());//获取图片流                
            }

            catch (Exception ex)
            {
                string aa = ex.Message;
            }
            finally
            {
                res.Close();
            }
            return img;
        }

        /// <summary>    
        /// Resize图片    
        /// </summary>    
        /// <param name="bmp">原始Bitmap</param>    
        /// <param name="newW">新的宽度</param>    
        /// <param name="newH">新的高度</param>    
        /// <param name="Mode">保留着，暂时未用</param>    
        /// <returns>处理以后的图片</returns>    
        public static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

    }
}
