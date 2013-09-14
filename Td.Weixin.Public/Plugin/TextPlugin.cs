/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-13 21:43:29
 *	Desc:	
 * 
*******************************/

using System.Text.RegularExpressions;
using System.Threading;
using Td.Weixin.Public.Message;

namespace Td.Weixin.Public.Plugin
{
    /// <summary>
    /// 处理文本消息的插件基类。
    /// <para>继承自类不是只能处理文本消息的，此类只用于为文本消息提供一些通用的处理逻辑。</para>
    /// </summary>
    public class TextPlugin : Plugin
    {
        protected string Pattern;

        protected TextPlugin(string pattern)
        {
            Pattern = pattern;
        }

        public override bool CanProcess(PluginContext ctx)
        {
            var t = ctx.ReceiveMessage as RecTextMessage;
            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
            var notExist = string.IsNullOrEmpty(Pattern);
            return t != null && (notExist || Regex.IsMatch(t.Content, Pattern, options));
        }
    }
}