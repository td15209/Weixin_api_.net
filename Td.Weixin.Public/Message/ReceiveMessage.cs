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


        #region 静态方法


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
        /// 从接收到的消息中获取信息以填充到响应消息中。
        /// </summary>
        /// <param name="msg"></param>
        private void FillRepMsg(ResponseMessage msg)
        {
            msg.FromUserName = ToUserName;
            msg.ToUserName = FromUserName;
        }

        /// <summary>
        /// 获取文本响应消息
        /// </summary>
        /// <returns></returns>
        public RepTextMessage GetTextResponse()
        {
            var ret = new RepTextMessage();
            FillRepMsg(ret);
            return ret;
        }

        /// <summary>
        /// 获取音乐响应消息
        /// </summary>
        public RepMusicMessage GetMusicResponse()
        {
            var ret = new RepMusicMessage();
            FillRepMsg(ret);
            return ret;
        }

        /// <summary>
        /// 获取图文响应消息
        /// </summary>
        /// <returns></returns>
        public RepNewsMessage GetNewsResponse()
        {
            var ret = new RepNewsMessage();
            FillRepMsg(ret);
            return ret;
        }
    }
}