/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-04 22:34:50
 *	Desc:	
 * 
*******************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;

namespace Td.Weixin.Public.Common
{
    public class HttpHelper
    {
        public static int Timeout = 10 * 1000;
        public string Url { get; set; }

        public static Encoding Encoding = Encoding.UTF8;


        public HttpHelper(string url)
        {
            Url = url;
        }


        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="formData"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(FormData formData)
        {
            return JsonConvert.DeserializeObject<T>(GetString(formData));
        }

        public T GetAnonymous<T>(FormData formData, T anonymous)
        {
            return Get<T>(formData);
        }

        public string GetString(FormData formData)
        {
            var url = string.Format("{0}?{1}", Url, formData.Format());
            return ReadFromResponse(GetStream(url));
        }


        /// <summary>
        /// 以POST方式提交数据。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="body"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public T Post<T>(string body, FormData formData = null)
        {
            var s = PostString(body, formData);
            return JsonConvert.DeserializeObject<T>(s);
        }

        /// <summary>
        /// 以post方式提交，将响应编码为字串。
        /// </summary>
        /// <param name="body"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public string PostString(string body, FormData formData)
        {
            var request = CreateRequest(string.Format("{0}?{1}", Url, formData == null ? "" : formData.Format()));
            request.Method = "POST";
            var sw = new StreamWriter(request.GetRequestStream());//注意，StreamWriter不能带编码的构造，否则会在前面写入编码标识(比utf8：ef bb bf)
            sw.Write(body);
            sw.Flush();
            sw.Close();;
            using (var rep = request.GetResponse().GetResponseStream())
            {
                var ret = ReadFromResponse(rep);
                return ret;
            }
        }

        /// <summary>
        /// 创建一个请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        internal static WebRequest CreateRequest(string url)
        {
            var ret = WebRequest.Create(url);
            ret.Timeout = Timeout;

            return ret;
        }

        private static T ReadFromResponse<T>(WebResponse rep)
        {
            var sm = rep.GetResponseStream();
            return ReadFromResponse<T>(sm);
        }

        private static T ReadFromResponse<T>(Stream stream)
        {
            var ret = JsonConvert.DeserializeObject<T>(ReadFromResponse(stream));
            return ret;
        }

        private static string ReadFromResponse(Stream stream)
        {
            if (stream == null)
                return null;

            var sr = new StreamReader(stream, Encoding);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 以GET方式获取指定url的响应流
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Stream GetStream(string url)
        {
            var request = CreateRequest(url);
            request.Method = "GET";
            return request.GetResponse().GetResponseStream();
        }

        /// <summary>
        /// 上传文件。formData参数附加到url
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string Upload(FormData formData, string filePath)
        {
            var url = string.Format("{0}?{1}", Url, formData == null ? "" : formData.Format());
            var data = new WebClient().UploadFile(url, "POST", filePath);
            return Encoding.GetString(data);
        }
    }

    public class FormData : Dictionary<string, object>
    {
        /// <summary>
        /// 转换为http form格式字符串
        /// </summary>
        /// <returns></returns>
        public virtual string Format()
        {
            var lst = this.Select(e => string.Format("{0}={1}", e.Key, Uri.EscapeUriString(Convert.ToString(e.Value))));
            return string.Join("&", lst);
        }
    }
}