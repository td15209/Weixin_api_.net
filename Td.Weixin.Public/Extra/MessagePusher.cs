using Td.Weixin.Public.Common;
using Td.Weixin.Public.Extra.Models;

namespace Td.Weixin.Public.Extra
{
    /// <summary>
    ///     用于发送“客户消息”
    /// </summary>
    public class MessagePusher
    {
        /// <summary>
        ///     默认接口地址
        /// </summary>
        public const string DefaultUrl = "https://api.weixin.qq.com/cgi-bin/message/custom/send";

        public MessagePusher()
            : this(null)
        {
        }

        /// <summary>
        ///     接口地址
        /// </summary>
        /// <param name="accessToken"></param>
        public MessagePusher(string accessToken)
        {
            Url = DefaultUrl;
            AccessToken = accessToken;
        }

        /// <summary>
        ///     默认的Pusher，使用默认接口地址和缓存的access_token
        /// </summary>
        public static MessagePusher Default
        {
            get { return new MessagePusher(Credential.CachedAccessToken); }
        }

        /// <summary>
        ///     接口地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     access_token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     推送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public BasicResult Push(MessageForPush msg)
        {
            var hh = new HttpHelper(Url);
            return hh.Post<BasicResult>(msg.ToString(), new FormData {{"access_token", AccessToken}});
        }
    }
}