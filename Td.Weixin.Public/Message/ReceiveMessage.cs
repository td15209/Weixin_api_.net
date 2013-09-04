/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-04 14:36:48
 *	Desc:	
 * 
*******************************/

using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Xml.Linq;

namespace Td.Weixin.Public.Message
{
    /// <summary>
    /// 接收的消息
    /// </summary>
    public class ReceiveMessage : Message
    {
        /// <summary>
        /// 消息id
        /// </summary>
        [Output]
        public long MsgId { get; set; }

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

        #region 静态方法

        private static void ParseMetadata(ReceiveMessage msg)
        {
            if (msg == null)
                return;

            var ps = msg.GetType().GetCustomAttributes(typeof(MusicMsgData), true)
                .Select(p => p as PropertyInfo);
            foreach (var p in ps)
            {
                var context = HttpContext.Current;
                if (context == null)
                    return;
                p.SetValue(msg, context.Request[p.Name], null);
            }
        }

        /// <summary>
        /// 从xml文件解析消息值(不包含签名信息)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ReceiveMessage Parse(string text)
        {
            var ret = ObtainByType(text);
            ret.ParseFrom(text);
            return ret;
        }

        /// <summary>
        /// 从HttpContext中获取信息并解析
        /// </summary>
        /// <returns></returns>
        public static ReceiveMessage ParseFromContext()
        {
            var request = HttpContext.Current.Request;
            var sr = new StreamReader(request.InputStream);
            var msg = Parse(sr.ReadToEnd());
            ParseMetadata(msg);

            return msg;
        }

        /// <summary>
        /// 为了简单，直接使用switch
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static ReceiveMessage ObtainByType(string text)
        {
            var e = XElement.Parse(text);
            var t = e.Element("MsgType").Value;
            switch (t)
            {
                case "text":
                    return new RecTextMessage();
                case "image":
                    return new RecImageMessage();
                case "location":
                    return new RecLocationMessage();
                case "link":
                    return new RecLinkMessage();
                case "event":
                    return new RecEventMessage();
            }
            return null;
        }
        #endregion

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Check(string token)
        {
            var vs = new[] { timestamp, nonce, token }.OrderBy(s => s);
            var str = string.Join("", vs);
            var copu = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");
            return signature == copu;
        }

        private void FillRepMsg(ResponseMessage msg)
        {
            msg.FromUserName = ToUserName;
            msg.ToUserName = FromUserName;
            msg.echostr = echostr;
        }

        /// <summary>
        /// 获取文本响应消息
        /// </summary>
        /// <returns></returns>
        public ResponseMessage GetTextResponse()
        {
            var ret = new RepTextMessage();
            FillRepMsg(ret);
            return ret;
        }

        /// <summary>
        /// 获取音乐响应消息
        /// </summary>
        public ResponseMessage GetMusicResponse()
        {
            var ret = new RepMusicMessage();
            FillRepMsg(ret);
            return ret;
        }

        /// <summary>
        /// 获取图文响应消息
        /// </summary>
        /// <returns></returns>
        public ResponseMessage GetNewsResponse()
        {
            var ret = new RepNewsMessage();
            FillRepMsg(ret);
            return ret;
        }
    }
}