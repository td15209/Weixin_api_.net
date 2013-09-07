using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace Td.Weixin.Public.Common
{
    /// <summary>
    /// 接入验证
    /// </summary>
    public class EntrySign
    {
        private static void ParseMetadata(EntrySign es)
        {
            if (es == null)
                return;

            var ps = es.GetType().GetProperties();
            foreach (var p in ps)
            {
                var context = HttpContext.Current;
                if (context == null)
                    return;
                p.SetValue(es, context.Request[p.Name], null);
            }
        }

        /// <summary>
        /// 检查当前请求是否是接入验证请求。
        /// 如果正确，请执行(实例).Response()方法。
        /// <para>如果为false，则表示已经成功接入，本次请求为消息处理请求。</para>
        /// </summary>
        /// <returns></returns>
        public static bool IsEntryCheck()
        {
            return "GET".Equals(HttpContext.Current.Request.HttpMethod);
        }

        /// <summary>
        /// 检查当前请求是否带有签名验证，一般情况都为true（表示需要验证。当然如果不考虑安全，也可以不验证）。
        /// <para>如果为true，则表示需要验证，此时请执行 EntrySign.ParseFromContext()获取实例然后执行 (实例).Check()方法以检测签名是否正确。
        /// </para>
        /// </summary>
        /// <returns></returns>
        public static bool IsSignCheckRequest()
        {
            return string.IsNullOrEmpty(HttpContext.Current.Request["signature"]);
        }

        /// <summary>
        /// 请上下文获取验证数据。
        /// </summary>
        /// <returns></returns>
        public static EntrySign ParseFromContext()
        {
            var ret = new EntrySign();
            ParseMetadata(ret);
            return ret;
        }

        /// <summary>
        /// 从配置文件获取token。节点名：WeixinToken
        /// </summary>
        /// <returns></returns>
        public static string GetTokenFromConfig()
        {
            return WebConfigurationManager.AppSettings["WeixinToken"];
        }

        /// <summary>
        /// 提交的签名
        /// </summary>
        public string signature { get; set; }

        public string timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string nonce { get; set; }

        /// <summary>
        /// 随机字符串。若确认此次GET请求来自微信服务器，请原样返回echostr参数内容，则接入生效，否则接入失败。
        /// </summary>
        public string echostr { get; set; }


        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="token">如果为空串或null，则从配置文件中获取</param>
        /// <returns></returns>
        public bool Check(string token = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                token = GetTokenFromConfig();
            }
            var vs = new[] { timestamp, nonce, token }.OrderBy(s => s);
            var str = string.Join("", vs);
            var copu = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");
            return copu.Equals(signature,StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 返回接入验证响应。
        /// 注意，请在调用Check()后再调用此方法。
        /// 调用此方法后，本次响应完成，不能再写入其它响应数据。
        /// </summary>
        public void Response()
        {
            var response = HttpContext.Current.Response;
            response.Write(echostr);
            response.End();
        }
    }
}