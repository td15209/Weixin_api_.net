/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 16:35:33
 *	Desc:	
 * 
*******************************/

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra
{
    /// <summary>
    /// 粉丝个人信息明细
    /// </summary>
    public class WxUserInfoFree
    {
        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        [JsonProperty(PropertyName = "fake_id")]
        public string FakeId { get; set; }

        [JsonProperty(PropertyName = "group_id")]
        public int GroupID { get; set; }


        public List<WxUserGroupFree> Groups { get; set; }

        [JsonProperty(PropertyName = "nick_name")]
        public string NickName { get; set; }


        public string ReMarkName { get; set; }

        /// <summary>
        /// 1：男; 2:女
        /// </summary>
        [JsonProperty(PropertyName = "gender")]
        public string Sex { get; set; }

        /// <summary>
        /// 个人签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 微信登陆名
        /// </summary>
        [JsonProperty(PropertyName = "user_name")]
        public string Username { get; set; }

        #region 额外
        public string OpenId { get; set; }
        #endregion

    }

    //todo:结构有调整
    public class WxUserGroupFree
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }
    }

    //新调整
    public class WxUserInfoRespFree
    {
        public object base_resp { get; set; }
        public object groups { get; set; }
        public WxUserInfoFree contact_info { get; set; }
    }
}