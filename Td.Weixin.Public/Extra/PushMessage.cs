/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	$date$
 *	Desc:	
 * 
*******************************/

using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra
{
    /// <summary>
    /// 24小时内推送的消息
    /// </summary>
    public class PushMessage
    {
        #region 常量定义

        /// <summary>
        /// 文本消息
        /// </summary>
        public const string TextMsg = "text";

        /// <summary>
        /// 图片消息
        /// </summary>
        public const string ImageMsg = "image";

        /// <summary>
        /// 语音消息
        /// </summary>
        public const string VoiceMsg = "voice";

        /// <summary>
        /// 视频消息
        /// </summary>
        public const string VideoMsg = "video";

        /// <summary>
        /// 音乐消息
        /// </summary>
        public const string MusicMsg = "music";

        /// <summary>
        /// 图文消息
        /// </summary>
        public const string NewsMsg = "news";

        #endregion


        /// <summary>
        /// 接收消息的用户的openid
        /// </summary>
        [JsonProperty(PropertyName = "touser")]
        public string ToUser { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty(PropertyName = "msgtype")]
        public string MsgType { get; protected set; }

        /// <summary>
        /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
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
}