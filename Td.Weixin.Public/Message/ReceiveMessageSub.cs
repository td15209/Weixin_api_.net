/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-04 15:12:18
 *	Desc:	
 * 
*******************************/

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Td.Weixin.Public.Message
{
    /// <summary>
    /// 接收的文本消息
    /// </summary>
    public class RecTextMessage : ReceiveMessage
    {
        public RecTextMessage()
        {
            MsgType = MessageType.Text;
        }

        [Output]
        public string Content { get; set; }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    public class RecImageMessage : ReceiveMessage
    {
        public RecImageMessage()
        {
            MsgType = MessageType.Image;
        }

        [Output]
        public string PicUrl { get; set; }
    }

    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class RecLocationMessage : ReceiveMessage
    {
        public RecLocationMessage()
        {
            MsgType = MessageType.Location;
        }

        /// <summary>
        /// X 坐标
        /// </summary>
        [Output]
        public float Location_X { get; set; }

        /// <summary>
        /// Y 坐标
        /// </summary>
        [Output]
        public float Location_Y { get; set; }

        /// <summary>
        /// 缩放级别
        /// </summary>
        [Output]
        public int Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        [Output]
        public string Label { get; set; }
    }

    /// <summary>
    /// 链接消息
    /// </summary>
    public class RecLinkMessage : ReceiveMessage
    {
        public RecLinkMessage()
        {
            MsgType = MessageType.Link;
        }

        /// <summary>
        /// 消息标题
        /// </summary>
        [Output]
        public string Title { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        [Output]
        public string Description { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        [Output]
        public string Url { get; set; }
    }

    /// <summary>
    /// 事件推送消息
    /// </summary>
    public class RecEventMessage : ReceiveMessage
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        public const string Subscribe = "subscribe";

        /// <summary>
        /// 退订事件
        /// </summary>
        public const string Unsubscribe = "unsubscribe";

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        public const string Click = "CLICK";

        public RecEventMessage()
        {
            MsgType = MessageType.Event;
        }

        /// <summary>
        /// 事件类型。从常量中获取
        /// </summary>
        [Output]
        public string Event { get; set; }

        /// <summary>
        /// 事件Key
        /// </summary>
        [Output]
        public string EventKey { get; set; }
    }
}