using Td.Weixin.Public.Message;

namespace Td.Weixin.Public.Plugin
{
    /// <summary>
    ///     响应菜单事件的插件基类。
    /// </summary>
    public class MenuKeyPlugin : Plugin
    {
        protected MenuKeyPlugin(string key)
        {
            Key = key;
        }

        public string Key { get; private set; }

        public override bool CanProcess(PluginContext ctx)
        {
            var msg = ctx.ReceiveMessage;
            return msg is RecEventMessage && Key.Equals((msg as RecEventMessage).EventKey);
        }
    }
}