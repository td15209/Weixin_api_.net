using System.Collections.Generic;
using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra.ModelsFree
{
    /// <summary>
    ///     粉丝个人信息明细
    /// </summary>
    public class WxUserDetail
    {
        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        [JsonProperty(PropertyName = "fake_id")]
        public string FakeId { get; set; }

        [JsonProperty(PropertyName = "group_id")]
        public int GroupID { get; set; }


        public List<WxGroup> Groups { get; set; }

        [JsonProperty(PropertyName = "nick_name")]
        public string NickName { get; set; }


        public string ReMarkName { get; set; }

        /// <summary>
        ///     1：男; 2:女
        /// </summary>
        [JsonProperty(PropertyName = "gender")]
        public string Sex { get; set; }

        /// <summary>
        ///     个人签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        ///     微信登陆名
        /// </summary>
        [JsonProperty(PropertyName = "user_name")]
        public string Username { get; set; }

        #region 额外

        public string OpenId { get; set; }

        #endregion
    }


    /// <summary>
    ///     模拟登陆后获取用户明细的响应结构
    /// </summary>
    public class WxUserDetailResult
    {
        public CommonResult base_resp { get; set; }
        internal WxUserDetailResultGroup groups { get; set; }
        public WxUserDetail contact_info { get; set; }
    }

    /// <summary>
    ///     模拟获取用户详细信息时的响应结构中的用户所在组的结构
    /// </summary>
    internal class WxUserDetailResultGroup
    {
        public List<WxGroup> groups { get; set; }
    }
}