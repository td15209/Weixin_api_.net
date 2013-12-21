/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 10:25:12
 *	Desc:	
 * 
*******************************/

using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra
{
    /// <summary>
    /// 微信粉丝概要
    /// </summary>
    public class WxUserFree
    {
        /// <summary>
        /// 即fakeid
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "nick_name")]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "remark_name")]
        public string RemarkName { get; set; }

        [JsonProperty(PropertyName = "group_id")]
        public int GroupID { get; set; }
    }
}