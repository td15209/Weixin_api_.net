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
        public static int Timeout = 10*1000;

        public static Encoding Encoding = Encoding.UTF8;

        public HttpHelper(string url)
        {
            Url = url;
        }

        public string Url { get; set; }


        /// <summary>
        ///     发送Get请求
        /// </summary>
        /// <param name="formData"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(FormData formData)
        {
            return Deserialize<T>(GetString(formData));
        }

        /// <summary>
        ///     请GET方式请求，将响应序列化为<paramref name="anonymous" />的结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="formData"></param>
        /// <param name="anonymous"></param>
        /// <returns></returns>
        public T GetAnonymous<T>(FormData formData, T anonymous)
        {
            return Get<T>(formData);
        }

        /// <summary>
        ///     以GET方式请求，返回响应的字符中
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        public string GetString(FormData formData)
        {
            return GetString(Url, formData);
        }


        /// <summary>
        ///     以POST方式提交数据。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="body"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public T Post<T>(string body, FormData formData = null)
        {
            var url = String.Format("{0}{1}", Url, formData == null ? "" : string.Format("?{0}", formData.Format()));
            return Post<T>(url, body);
        }

        /// <summary>
        ///     以post方式提交，将响应编码为字串。
        /// </summary>
        /// <param name="body"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public string PostString(string body, FormData formData = null)
        {
            var url = String.Format("{0}{1}", Url, formData == null ? "" : string.Format("?{0}", formData.Format()));
            return PostString(url, body);
        }


        /// <summary>
        ///     上传文件。formData参数附加到url
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string Upload(FormData formData, string filePath)
        {
            var url = String.Format("{0}?{1}", Url, formData == null ? "" : formData.Format());
            var data = new WebClient().UploadFile(url, "POST", filePath);
            return Encoding.GetString(data);
        }

        /// <summary>
        ///     GET 方式获取响应流
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <param name="cc"></param>
        /// <param name="refer"></param>
        /// <returns></returns>
        public static Stream Get(string url, string queryString = null, CookieContainer cc = null, string refer = null)
        {
            url = String.Format("{0}?{1}", url, queryString);
            var r = CreateHttpWebRequest(url);
            r.Referer = refer;
            r.CookieContainer = cc;
            r.Method = "GET";
            return r.GetResponse().GetResponseStream();
        }

        public static Stream Get(string url, IEnumerable<KeyValuePair<string, object>> data = null,
            CookieContainer cc = null, string refer = null)
        {
            return Get(url, FormatData(data), cc, refer);
        }

        public static T Get<T>(string url, IEnumerable<KeyValuePair<string, object>> data = null,
            CookieContainer cc = null, string refer = null)
        {
            var s = FormatData(data);
            var json = new StreamReader(Get(url, s, cc, refer), Encoding).ReadToEnd();
            return Deserialize<T>(json);
        }

        public static string GetString(string url, string queryString = null, CookieContainer cc = null,
            string refer = null)
        {
            return new StreamReader(Get(url, queryString, cc, refer)).ReadToEnd();
        }

        /// <summary>
        ///     以GET方式请求，返回响应的字符中
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="cc"></param>
        /// <param name="refer"></param>
        /// <returns></returns>
        public static string GetString(string url, IEnumerable<KeyValuePair<string, object>> data = null,
            CookieContainer cc = null, string refer = null)
        {
            return new StreamReader(Get(url, FormatData(data), cc, refer)).ReadToEnd();
        }


        /// <summary>
        ///     POST 方式获取响应流
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data">写入正文部分的数据</param>
        /// <param name="cc"></param>
        /// <param name="refer"></param>
        /// <returns></returns>
        public static Stream Post(string url, string data = null, CookieContainer cc = null, string refer = null)
        {
            var r = CreateHttpWebRequest(url);
            r.Method = "POST";
            r.Referer = refer;
            r.CookieContainer = cc;
            r.ContentType = "application/x-www-form-urlencoded";
            if (data != null)
            {
                var bs = Encoding.GetBytes(data);
                r.ContentLength = bs.Length;
                var stream = r.GetRequestStream();
                stream.Write(bs, 0, bs.Length);
                stream.Flush();
                stream.Close();
            }
            var rep = r.GetResponse();
            return rep.GetResponseStream();
        }


        public static T Post<T>(string url, string data = null, CookieContainer cc = null, string refer = null)
        {
            var str = new StreamReader(Post(url, data, cc, refer), Encoding).ReadToEnd();
            return Deserialize<T>(str);
        }

        public static T Post<T>(string url, IEnumerable<KeyValuePair<string, object>> data = null,
            CookieContainer cc = null, string refer = null)
        {
            var s = FormatData(data);
            return Post<T>(url, s, cc, refer);
        }

        public static string PostString(string url, string data = null, CookieContainer cc = null, string refer = null)
        {
            var str = new StreamReader(Post(url, data, cc, refer), Encoding).ReadToEnd();
            return str;
        }

        public static HttpStatusCode HeadHttpCode(string url, string data = null,
            CookieContainer cc = null, string refer = null)
        {
            try
            {
                var r = CreateHttpWebRequest(url);
                r.Method = "HEAD";
                r.Referer = refer;
                r.CookieContainer = cc;
                return (r.GetResponse() as HttpWebResponse).StatusCode;
            }
            catch
            {
                return HttpStatusCode.ExpectationFailed;
            }
        }

        /// <summary>
        ///     创建一个请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpWebRequest CreateRequest(string url)
        {
            var r = WebRequest.Create(url) as HttpWebRequest;
            return r;
        }

        /// <summary>
        ///     创建一个请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static HttpWebRequest CreateHttpWebRequest(string url)
        {
            var r = CreateRequest(url);
            r.Timeout = Timeout;
            //r.Headers["X-Requested-With"] = "XMLHttpRequest";
            var s =
                "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Maxthon/4.1.2.4000 Chrome/26.0.1410.43 Safari/537.1";
            r.UserAgent = s;
            return r;
        }

        /// <summary>
        ///     将json反序列化为对象。
        ///     如果参数json不为null（或空串）但不能序列化为类型T，将抛出DesializeException异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        private static T Deserialize<T>(string json)
        {
            var ret = JsonConvert.DeserializeObject<T>(json);

            //抛出异常
            if (!string.IsNullOrEmpty(json) && ret == null)
                throw new DesializeException(string.Format("不能反序列化字符串为类型 ‘{0}’", typeof (T)), json);

            return ret;
        }

        private static string FormatData(IEnumerable<KeyValuePair<string, object>> data)
        {
            return new FormData(data).Format();
        }
    }

    /// <summary>
    ///     不能将指定json字符串反序列化为指定类型时的异常
    /// </summary>
    public class DesializeException : ApplicationException
    {
        public DesializeException(string message, string sourceJson)
            : base(message)
        {
            SourceJson = sourceJson;
        }

        /// <summary>
        ///     不能反序列化的json字符串
        /// </summary>
        public string SourceJson { get; set; }
    }

    public class FormData : Dictionary<string, object>
    {
        public FormData()
        {
        }

        public FormData(IEnumerable<KeyValuePair<string, object>> data)
        {
            foreach (var keyValuePair in data)
            {
                Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        /// <summary>
        ///     转换为http form格式字符串
        /// </summary>
        /// <returns></returns>
        public virtual string Format()
        {
            var lst = this.Select(e => String.Format("{0}={1}", e.Key, Uri.EscapeDataString(Convert.ToString(e.Value))));
            return String.Join("&", lst);
        }
    }
}