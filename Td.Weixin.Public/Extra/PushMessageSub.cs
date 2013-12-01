/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	$date$
 *	Desc:	推送的消息的具体类
 * 
*******************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra
{
    /// <summary>
    /// （推送）文本消息
    /// </summary>
    public class PushTextMessage : PushMessage
    {
        public PushTextMessage()
        {
            MsgType = TextMsg;
        }

        [JsonProperty(PropertyName = "text")]
        public PushMsgTextData Text { get; set; }

    }

    /// <summary>
    /// （推送）图片消息
    /// </summary>
    public class PushImageMessage : PushMessage
    {
        public PushImageMessage()
        {
            MsgType = ImageMsg;
        }

        [JsonProperty(PropertyName = "image")]
        public PushMsgImageData Image { get; set; }
    }

    /// <summary>
    /// （推送）语音消息
    /// </summary>
    public class PushVoiceMessage : PushMessage
    {
        public PushVoiceMessage()
        {
            MsgType = VoiceMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "voice")]
        public PushMsgVoiceData Voice { get; set; }
    }

    /// <summary>
    /// （推送）视频消息
    /// </summary>
    public class PushVideoMessage : PushMessage
    {
        public PushVideoMessage()
        {
            MsgType = VideoMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "video")]
        public PushMsgVideoData Voice { get; set; }
    }

    /// <summary>
    /// （推送）音乐消息
    /// </summary>
    public class PushMusicMessage : PushMessage
    {
        public PushMusicMessage()
        {
            MsgType = MusicMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "music")]
        public PushMsgMusicData Music { get; set; }
    }

    /// <summary>
    /// (可选)图文消息
    /// </summary>
    public class PushNewsMessage : PushMessage
    {
        public PushNewsMessage()
        {
            MsgType = NewsMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "news")]
        public PushMsgNewsData News { get; set; }
    }
}
