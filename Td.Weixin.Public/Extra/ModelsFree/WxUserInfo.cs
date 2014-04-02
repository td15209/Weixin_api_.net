using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra.ModelsFree
{
    /// <summary>
    ///     微信粉丝概要
    /// </summary>
    public class WxUserInfo
    {
        /// <summary>
        ///     即fakeid
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        /// <summary>
        ///     昵称
        /// </summary>
        [JsonProperty(PropertyName = "nick_name")]
        public string NickName { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [JsonProperty(PropertyName = "remark_name")]
        public string RemarkName { get; set; }

        /// <summary>
        ///     所在组id
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public int GroupID { get; set; }
    }
}