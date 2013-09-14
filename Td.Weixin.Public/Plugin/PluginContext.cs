/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-13 21:23:11
 *	Desc:	
 * 
*******************************/

using Td.Weixin.Public.Common;
using Td.Weixin.Public.Message;

namespace Td.Weixin.Public.Plugin
{
    public class PluginContext
    {
        /// <summary>
        /// 收到的消息
        /// </summary>
        public ReceiveMessage ReceiveMessage { get; set; }

        /// <summary>
        /// 商家信息
        /// </summary>
        public SellerContext Seller { get; set; }

        /// <summary>
        /// 用户相关信息
        /// </summary>
        public UserContext UserContext { get; set; }

        /// <summary>
        /// 如果某插件被匹配，但当前商家没有启用此插件，需要中断此次请求时，请将此值设为true。
        /// <para>预留。考虑是:中断此次请求意味着不会给用户任何响应信息，这是不友好的方式。</para>
        /// </summary>
        public bool Cancel { get; set; }
    }
}