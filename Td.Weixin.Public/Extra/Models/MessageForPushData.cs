/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-12-01 22:03:19
 *	Desc:	官方接口发送的消息的消息数据
 * 
*******************************/

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra.Models
{
    public class MsgDataForPush
    {
        /// <summary>
        ///     返回表示当前 <see cref="T:System.Object" /> 的 <see cref="T:System.String" />。
        /// </summary>
        /// <returns>
        ///     <see cref="T:System.String" />，表示当前的 <see cref="T:System.Object" />。
        /// </returns>
        public override string ToString()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(this, settings);
        }
    }

    /// <summary>
    ///     文本消息数据
    /// </summary>
    public class TextMsgDataForPush : MsgDataForPush
    {
        /// <summary>
        ///     文本消息内容
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        public static implicit operator TextMsgDataForPush(string content)
        {
            return new TextMsgDataForPush
            {
                Content = content
            };
        }
    }

    /// <summary>
    ///     图片消息数据
    /// </summary>
    public class ImageMsgDataForPush : MsgDataForPush
    {
        /// <summary>
        ///     发送的图片的媒体ID
        /// </summary>
        [JsonProperty(PropertyName = "media_id")]
        public string MediaID { get; set; }

        public static implicit operator ImageMsgDataForPush(string mediaId)
        {
            return new ImageMsgDataForPush
            {
                MediaID = mediaId
            };
        }
    }

    /// <summary>
    ///     语音消息数据
    /// </summary>
    public class VoiceMsgDataForPush : MsgDataForPush
    {
        /// <summary>
        ///     发送的语音的媒体ID
        /// </summary>
        [JsonProperty(PropertyName = "media_id")]
        public string MediaID { get; set; }

        public static implicit operator VoiceMsgDataForPush(string mediaId)
        {
            return new VoiceMsgDataForPush
            {
                MediaID = mediaId
            };
        }
    }

    /// <summary>
    ///     视频消息数据
    /// </summary>
    public class VideoMsgDataForPush : MsgDataForPush
    {
        /// <summary>
        ///     发送的视频的媒体ID
        /// </summary>
        [JsonProperty(PropertyName = "media_id")]
        public string MediaID { get; set; }

        /// <summary>
        ///     缩略图ID
        /// </summary>
        [JsonProperty(PropertyName = "thumb_media_id")]
        public string ThumbMediaID { get; set; }

        /// <summary>
        ///     (可选)视频消息的标题
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        ///     （可选）视频消息的描述
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }

    /// <summary>
    ///     音乐消息数据
    /// </summary>
    public class MusicMsgDataForPush : MsgDataForPush
    {
        /// <summary>
        ///     （可选）标题
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        ///     （可选）音乐描述
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     音乐链接
        /// </summary>
        [JsonProperty(PropertyName = "musicurl")]
        public string MusicUrl { get; set; }

        /// <summary>
        ///     高品质音乐链接，wifi环境优先使用该链接播放音乐
        /// </summary>
        [JsonProperty(PropertyName = "hqmusicurl")]
        public string HqMusicUrl { get; set; }

        /// <summary>
        ///     缩略图的媒体ID
        /// </summary>
        [JsonProperty(PropertyName = "thumb_media_id")]
        public string ThumbMediaID { get; set; }
    }

    /// <summary>
    ///     图文消息数据
    /// </summary>
    public class NewsMsgDataForPush : MsgDataForPush
    {
        public NewsMsgDataForPush()
        {
            Articles = new List<NewsMsgDataItemForPush>();
        }

        /// <summary>
        ///     图文消息条目。
        ///     条数限制在10条以内，注意，如果图文数超过10，则将会无响应。
        /// </summary>
        [JsonProperty(PropertyName = "articles")]
        public List<NewsMsgDataItemForPush> Articles { get; private set; }
    }

    public class NewsMsgDataItemForPush
    {
        /// <summary>
        ///     标题
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     (可选) 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
        /// </summary>
        [JsonProperty(PropertyName = "picurl")]
        public string PicUrl { get; set; }

        /// <summary>
        ///     (可选）点击后跳转的链接
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}