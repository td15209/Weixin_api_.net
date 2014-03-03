/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 10:25:02
 *	Desc:	
 * 
*******************************/

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Td.Weixin.Public.Extra.Models
{
    /// <summary>
    ///     粉丝分组
    /// </summary>
    public class WxGroup
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     组下用户数
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }

    /// <summary>
    ///     “查询分组”接口的返回结构
    /// </summary>
    public class WxGroupQueryResult
    {
        [JsonProperty(PropertyName = "groups")]
        public List<WxGroup> Groups { get; set; }
    }

    /// <summary>
    ///     “创建分组”接口返回结构
    /// </summary>
    public class WxGroupCreateResult
    {
        [JsonProperty(PropertyName = "group")]
        public WxGroup Group { get; set; }
    }
}