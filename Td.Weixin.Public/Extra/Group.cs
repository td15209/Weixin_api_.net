/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 10:25:02
 *	Desc:	
 * 
*******************************/

using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra
{
    /// <summary>
    /// 粉丝分组
    /// </summary>
    public class WxGroup
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 组下用户数
        /// </summary>
        [JsonProperty(PropertyName = "cnt")]
        public int Cnt { get; set; }
    }
}