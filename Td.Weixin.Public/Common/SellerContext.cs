/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-13 22:17:42
 *	Desc:	
 * 
*******************************/

namespace Td.Weixin.Public.Common
{
    /// <summary>
    /// 商家信息。接入平台的拥有微信公号的商家的相关信息。
    /// <para>可通过继承或partial方式扩展以存储更多的信息。</para>
    /// </summary>
    public partial class SellerContext
    {
        /// <summary>
        /// 商家在系统内的ID。如果不是int型的，可以通过继承扩展。
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 商家token信息，用于验证签名。
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// appid
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// app secret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 额外信息。可用于存储一些其它信息。
        /// </summary>
        public object Tag { get; set; }
    }
}