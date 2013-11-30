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
        public double Location_X { get; set; }

        /// <summary>
        /// Y 坐标
        /// </summary>
        [Output]
        public double Location_Y { get; set; }

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

        /// <summary>
        /// 扫描二维码事件
        /// </summary>
        public const string Scan = "scan";

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        public const string Location = "LOCATION";

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
        /// 事件Key。
        /// <para>用户通过扫描带参二维码订阅时：qrscene_为前缀，后面为二维码的参数值；</para>
        /// <para>扫描带参二维码事件：一个32位无符号整数</para>
        /// </summary>
        [Output]
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片。
        /// 仅在扫描带参二维码时有值。
        /// </summary>
        [Output]
        public string Ticket { get; set; }

        /// <summary>
        /// 地理位置纬度。
        /// 仅在“位置”事件时有意义
        /// </summary>
        [Output]
        public double Latitude { get; set; }

        /// <summary>
        /// 地理位置经度。
        /// 仅在“位置”事件时有意义
        /// </summary>
        [Output]
        public double Longitude { get; set; }

        /// <summary>
        /// 地理位置精度。
        /// 仅在“位置”事件时有意义
        /// </summary>
        [Output]
        public double Precision { get; set; }
    }

    /// <summary>
    /// 语音消息
    /// </summary>
    public class RecVoiceMessage : ReceiveMessage
    {
        public RecVoiceMessage()
        {
            MsgType = MessageType.Voice;
        }

        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取该媒体
        /// </summary>
        [Output]
        public string MediaID { get; set; }

        /// <summary>
        /// 语音格式：amr
        /// </summary>
        [Output]
        public string Format { get; set; }

        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>
        [Output]
        public string Recognition { get; set; }
    }

    /// <summary>
    /// 视频消息
    /// </summary>
    public class RecVideoMessage : ReceiveMessage
    {
        public RecVideoMessage()
        {
            MsgType = MessageType.Video;
        }

        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        [Output]
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        [Output]
        public string ThumbMediaId { get; set; }
    }
}