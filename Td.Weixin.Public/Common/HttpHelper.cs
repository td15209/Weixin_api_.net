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
            var request = WebRequest.Create(string.Format("{0}?{1}", Url, formData.Format()));
            request.Method = "GET";
            T ret;
            using (var rep = request.GetResponse())
            {
                ret = ReadFromResponse<T>(rep);
            }
            return ret;
        }

        public T GetAnonymous<T>(FormData formData, T anonymous)
        {
            return Get<T>(formData);
        }

        public T Post<T>(string body, FormData formData = null)
        {
            var request = WebRequest.Create(string.Format("{0}?{1}", Url, formData == null ? "" : formData.Format()));
            request.Method = "POST";
            var sw = new StreamWriter(request.GetRequestStream());
            sw.WriteLine(body);
            sw.Flush();
            sw.Close();
            T ret;
            using (var rep = request.GetResponse())
            {
                ret = ReadFromResponse<T>(rep);
            }
            return ret;
        }

        private static T ReadFromResponse<T>(WebResponse rep)
        {
            T ret = default(T);
            var sm = rep.GetResponseStream();
            if (sm != null)
            {
                var sr = new StreamReader(sm);
                ret = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
            }
            return ret;
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