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
using System.Web;
using Newtonsoft.Json;

namespace Td.Weixin.Public.Common
{
    public class HttpHelper
    {
        public string Url { get; set; }

        public static int Timeout = 10 * 1000;

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
            var request = CreateRequest(string.Format("{0}?{1}", Url, formData == null ? "" : formData.Format()));
            request.Method = "POST";
            var sw = new StreamWriter(request.GetRequestStream());
            sw.Write(body);
            sw.Flush();
            sw.Close();
            T ret;
            using (var rep = request.GetResponse())
            {
                ret = ReadFromResponse<T>(rep);
            }
            return ret;
        }

        private static WebRequest CreateRequest(string url)
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


        private static string ReadFromResponse(WebResponse rep)
        {
            var sm = rep.GetResponseStream();
            return ReadFromResponse(sm);
        }
        private static string ReadFromResponse(Stream stream)
        {
            if (stream == null)
                return null;

            var sr = new StreamReader(stream);
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