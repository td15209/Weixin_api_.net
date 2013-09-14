/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-13 21:16:14
 *	Desc:	
 * 
*******************************/

using Td.Weixin.Public.Message;

namespace Td.Weixin.Public.Plugin
{
    /// <summary>
    /// Invoker的上下文环境
    /// </summary>
    public class InvokerContext
    {
        public ReceiveMessage Message { get; set; }
    }
}