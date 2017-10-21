using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Text;
using System.IO;

namespace Common
{
    /// <summary>
    /// 微信素材上传
    /// </summary>
    public  class WXUploadMultimedia
    {

        /// <summary>
        /// 上传临时素材
        /// </summary>
        /// <param name="ACCESS_TOKEN"></param>
        /// <param name="Type"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static string UploadTemp(string Type, byte[] ms)
        {
            //string ACCESS_TOKEN = PubConstant.GetToken();
            string ACCESS_TOKEN =string.Empty;
            string result = "";
            string wxurl = "https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + ACCESS_TOKEN + "&type=" + Type;
            
            try
            {
                result= HttpUploadFile(wxurl, "ad.jpg", ms);
            }
            catch (Exception ex)
            {
                
               result = "Error:" + ex.Message;
            }
            return result;
        }

        public static string HttpUploadFile(string url, string fileName, byte[] bf)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            //int pos = path.LastIndexOf("\\");
            //string fileName = path.Substring(pos + 1);
            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"media\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bf, 0, bf.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Dispose();
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            string content = sr.ReadToEnd();
            sr.Dispose();
            instream.Dispose();
            response.Close();
            return content;
        }
    }

    public class TempMediaResult 
    {
        public string type { get; set; }

        public string media_id { get; set; }

        public long created_at { get; set; }
    }
}
