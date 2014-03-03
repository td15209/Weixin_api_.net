/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-11-30 22:40:07
 *	Desc:	
 * 
*******************************/

using System;
using Newtonsoft.Json;

namespace Td.Weixin.Public.Common
{
    /// <summary>
    ///     微信基本的响应结构
    /// </summary>
    public class BasicResult
    {
        /// <summary>
        ///     结果码
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public int ErrCode { get; set; }

        /// <summary>
        ///     结果文本说明
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string ErrMsg { get; set; }

        /// <summary>
        ///     是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return ErrCode == 0; }
        }

        public static BasicResult GetSuccess(string msg = null)
        {
            return new BasicResult {ErrCode = 0, ErrMsg = msg ?? "完成"};
        }

        public static BasicResult GetFailed(string msg, Exception ex = null)
        {
            return new BasicResult
            {
                ErrCode = -1,
                ErrMsg = string.Format("{0}\n{1}", msg, ex != null ? "发生异常：" + ex.Message : string.Empty)
            };
        }
    }
}