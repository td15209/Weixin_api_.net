using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra.Models
{
    /// <summary>
    ///     （推送）文本消息
    /// </summary>
    public class TextMessageForPush : MessageForPush
    {
        public TextMessageForPush()
        {
            MsgType = TextMsg;
        }

        [JsonProperty(PropertyName = "text")]
        public TextMsgDataForPush Text { get; set; }
    }

    /// <summary>
    ///     （推送）图片消息
    /// </summary>
    public class ImageMessageForPush : MessageForPush
    {
        public ImageMessageForPush()
        {
            MsgType = ImageMsg;
        }

        /// <summary>
        ///     图片数据
        /// </summary>
        [JsonProperty(PropertyName = "image")]
        public ImageMsgDataForPush Image { get; set; }
    }

    /// <summary>
    ///     （推送）语音消息
    /// </summary>
    public class VoiceMessageForPush : MessageForPush
    {
        public VoiceMessageForPush()
        {
            MsgType = VoiceMsg;
        }

        /// <summary>
        ///     语音数据
        /// </summary>
        [JsonProperty(PropertyName = "voice")]
        public VoiceMsgDataForPush Voice { get; set; }
    }

    /// <summary>
    ///     （推送）视频消息
    /// </summary>
    public class VideoMessageForPush : MessageForPush
    {
        public VideoMessageForPush()
        {
            MsgType = VideoMsg;
        }

        /// <summary>
        ///     视频数据
        /// </summary>
        [JsonProperty(PropertyName = "video")]
        public VideoMsgDataForPush Voice { get; set; }
    }

    /// <summary>
    ///     （推送）音乐消息
    /// </summary>
    public class MusicMessageForPush : MessageForPush
    {
        public MusicMessageForPush()
        {
            MsgType = MusicMsg;
        }

        /// <summary>
        ///     音乐数据
        /// </summary>
        [JsonProperty(PropertyName = "music")]
        public MusicMsgDataForPush Music { get; set; }
    }

    /// <summary>
    ///     (可选)图文消息
    /// </summary>
    public class NewsMessageForPush : MessageForPush
    {
        public NewsMessageForPush()
        {
            MsgType = NewsMsg;
        }

        /// <summary>
        ///     图文数据
        /// </summary>
        [JsonProperty(PropertyName = "news")]
        public NewsMsgDataForPush News { get; set; }
    }
}