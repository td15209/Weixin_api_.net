/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-13 23:27:53
 *	Desc:	
 * 
*******************************/

using Td.Weixin.Public.Message;

namespace Td.Weixin.Public.Plugin
{
    /// <summary>
    /// 响应菜单事件的插件基类。
    /// </summary>
    public class MenuKeyPlugin : Plugin
    {
        public string Key { get; private set; }

        protected MenuKeyPlugin(string key)
        {
            Key = key;
        }

        public override bool CanProcess(PluginContext ctx)
        {
            var msg = ctx.ReceiveMessage;
            return msg is RecEventMessage && Key.Equals((msg as RecEventMessage).EventKey);
        }
    }
}