/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com]
 *	Date:	2013-09-04 13:20:48
 *	Desc:	
 * 
*******************************/

using System;
using System.Linq;

namespace Td.Weixin.Public.Message
{
    /// <summary>
    /// 接收或响应的消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        [MessageType(TypeName = "text")]
        Text,

        /// <summary>
        /// 图片消息
        /// </summary>
        [MessageType(TypeName = "image")]
        Image,

        /// <summary>
        /// 地理位置消息
        /// </summary>
        [MessageType(TypeName = "location")]
        Location,

        /// <summary>
        /// 链接消息
        /// </summary>
        [MessageType(TypeName = "link")]
        Link,

        /// <summary>
        /// 事件推送消息
        /// </summary>
        [MessageType(TypeName = "event")]
        Event,

        /// <summary>
        /// 音乐消息（用于响应）
        /// </summary>
        [MessageType(TypeName = "music")]
        Music,

        /// <summary>
        /// 图文消息
        /// </summary>
        [MessageType(TypeName = "news")]
        News,

        /// <summary>
        /// 语音消息（用户的语音，可以使用TTS引擎分析成文本）
        /// </summary>
        [MessageType(TypeName = "voice")]
        Voice,

        /// <summary>
        /// 视频消息
        /// </summary>
        [MessageType(TypeName = "video")]
        Video
    }


    /// <summary>
    /// 微信平台接受的消息类型名称
    /// </summary>
    public class MessageTypeAttribute : Attribute
    {
        public string TypeName { get; set; }

        #region 静态方法

        /// <summary>
        /// 获取指定消息类型的微信平台接受名称
        /// </summary>
        /// <param name="mtype"></param>
        /// <returns></returns>
        public static string ObtainMessageType(MessageTypeAttribute mtype)
        {
            return mtype.TypeName;
        }

        /// <summary>
        /// 获取消息枚举对应的微信平台接受的消息名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ObtainMessageType(MessageType type)
        {
            var mi = type.GetType().GetMember(Enum.GetName(type.GetType(), type)).FirstOrDefault();
            MessageTypeAttribute attr = null;
            if (mi != null)
            {
                attr = mi.GetCustomAttributes(typeof(MessageTypeAttribute), true).FirstOrDefault() as MessageTypeAttribute;
            }
            return attr == null ? null : ObtainMessageType(attr);
        }
        #endregion
    }
}