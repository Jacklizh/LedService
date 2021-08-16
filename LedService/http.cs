using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace LedService
{
  public class http
    {
        /// <summary>
        /// 相机Post方法
        /// </summary>
        /// <param name="ServerIp"></param>
        /// <param name="port"></param>
        /// <param name="conent"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static string HttpPost(string ServerIp, string port, string url, string content)
        {
            string urlstr = "http://" + ServerIp + ":" + port + "/" + url;
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlstr);
            req.Method = "POST";
            req.Timeout = 10000;//超时时间10s
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] data = Encoding.GetEncoding("GBK").GetBytes(content.ToString());//编码格式为GBK
            req.ContentLength = data.Length;
            try
            {
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                System.Net.ServicePointManager.DefaultConnectionLimit = 50;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容  
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("GBK")))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        public static string HttpPost(string uri, string url, string content)
        {
            string urlstr = uri + url;
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlstr);
            req.Method = "POST";
            req.Timeout = 10000;//超时时间10s
            req.ContentType = "application/json";
            byte[] data = Encoding.UTF8.GetBytes(content.ToString());
            req.ContentLength = data.Length;
            try
            {
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                }
                //System.Net.ServicePointManager.DefaultConnectionLimit = 50;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                //接收客户端传过来的数据并转成字符串类型
                Stream stream = resp.GetResponseStream();
                //获取响应内容  
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }                
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
