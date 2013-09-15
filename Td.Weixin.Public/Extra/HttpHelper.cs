/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 10:53:54
 *	Desc:	
 * 
*******************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra
{
    public class HttpHelper
    {
        public static Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// GET 方式获取响应流
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <param name="cc"></param>
        /// <param name="refer"></param>
        /// <returns></returns>
        public static Stream Get(string url, string queryString = null, CookieContainer cc = null, string refer = null)
        {
            try
            {
                var r = WebRequest.Create(string.Format("{0}?{1}", url, queryString)) as HttpWebRequest;
                r.Referer = refer;
                r.CookieContainer = cc;
                r.Method = "GET";
                return r.GetResponse().GetResponseStream();
            }
            catch
            {
                return null;
            }
        }

        public static Stream Get(string url, IEnumerable<KeyValuePair<string, object>> data = null,
            CookieContainer cc = null, string refer = null)
        {
            return Get(url, FormatData(data), cc, refer);
        }

        public static T Get<T>(string url, IEnumerable<KeyValuePair<string, object>> data = null,
            CookieContainer cc = null, string refer = null)
        {
            try
            {
                var s = FormatData(data);
                return
                    JsonConvert.DeserializeObject<T>(
                        new StreamReader(Get(url, s, cc, refer), DefaultEncoding).ReadToEnd());
            }
            catch
            {
                return default(T);
            }
        }

        public static string GetString(string url, string queryString = null, CookieContainer cc = null,
            string refer = null)
        {
            return new StreamReader(Get(url, queryString, cc, refer)).ReadToEnd();
        }

        public static string GetString(string url, IEnumerable<KeyValuePair<string, object>> data = null,
            CookieContainer cc = null, string refer = null)
        {
            return new StreamReader(Get(url, FormatData(data), cc, refer)).ReadToEnd();
        }


        /// <summary>
        /// POST 方式获取响应流
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="cc"></param>
        /// <param name="refer"></param>
        /// <returns></returns>
        public static Stream Post(string url, string data = null, CookieContainer cc = null, string refer = null)
        {
            try
            {
                var r = WebRequest.Create(url) as HttpWebRequest;
                r.Method = "POST";
                r.Referer = refer;
                r.CookieContainer = cc;
                r.ContentType = "application/x-www-form-urlencoded";
                if (data != null)
                {
                    var bs = DefaultEncoding.GetBytes(data);
                    r.ContentLength = bs.Length;
                    var stream = r.GetRequestStream();
                    stream.Write(bs, 0, bs.Length);
                    stream.Flush();
                    stream.Close();
                }
                return r.GetResponse().GetResponseStream();
            }
            catch
            {
                return null;
            }
        }

        public static T Post<T>(string url, string data = null, CookieContainer cc = null, string refer = null)
        {
            try
            {
                var str = new StreamReader(Post(url, data, cc, refer), DefaultEncoding).ReadToEnd();
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch
            {
                return default(T);
            }
        }

        public static T Post<T>(string url, IEnumerable<KeyValuePair<string, object>> data = null,
            CookieContainer cc = null, string refer = null)
        {
            var s = FormatData(data);
            return Post<T>(url, s, cc, refer);
        }

        public static HttpStatusCode HeadHttpCode(string url, string data = null,
            CookieContainer cc = null, string refer = null)
        {
            try
            {
                var r = WebRequest.Create(url) as HttpWebRequest;
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

        private static string FormatData(IEnumerable<KeyValuePair<string, object>> data)
        {
            var s = data == null
                ? null
                : string.Join("&",
                    data.Select(d => string.Format("{0}={1}", d.Key, Uri.EscapeDataString(Convert.ToString(d.Value)))));
            return s;
        }
    }
}