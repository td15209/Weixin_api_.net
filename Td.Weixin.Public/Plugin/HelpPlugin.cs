/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-13 22:00:11
 *	Desc:	
 * 
*******************************/

using Td.Weixin.Public.Message;

namespace Td.Weixin.Public.Plugin
{
    public class HelpPlugin : TextPlugin
    {
        /// <summary>
        /// 默认匹配。可匹配关键字“帮助”，“help”，“h”。英文不区分大小写。
        /// </summary>
        public const string DefaultPattern = @"^\s*帮助|help|h\s*$";

        private bool _asDefault;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="asDefault">是否作为转为插件，处理所有消息</param>
        public HelpPlugin(string pattern, bool asDefault = false)
            : base(pattern)
        {
            _asDefault = asDefault;
        }

        public override bool CanProcess(PluginContext ctx)
        {
            return _asDefault || base.CanProcess(ctx);
        }

        public override ResponseMessage Execute(PluginContext ctx)
        {
            var seller = ctx.Seller;
            var rep = ctx.ReceiveMessage.GetTextResponse("系统使用帮助");
            return rep;
        }
    }
}