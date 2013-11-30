/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-11-30 22:40:07
 *	Desc:	
 * 
*******************************/

using Newtonsoft.Json;

namespace Td.Weixin.Public.Common
{
    /// <summary>
    /// 微信基本的响应结构
    /// </summary>
    public class BasicResult
    {
        /// <summary>
        /// 结果码
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public int ErrCode { get; set; }

        /// <summary>
        /// 结果文本说明
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string ErrMsg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return ErrCode == 0; }
        }
    }
}