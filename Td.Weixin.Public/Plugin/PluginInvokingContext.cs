using Td.Weixin.Public.Message;

namespace Td.Weixin.Public.Plugin
{
    /// <summary>
    ///     执行插件前的逻辑的参数
    /// </summary>
    public class PluginInvokingContext : PluginContext
    {
        public PluginInvokingContext()
        {
        }

        /// <summary>
        ///     从基类创建实例，复制基类的值（仅复制引用，这意味着修改参数值将影响到后续的传递）
        /// </summary>
        /// <param name="ctx"></param>
        public PluginInvokingContext(PluginContext ctx)
        {
            ReceiveMessage = ctx.ReceiveMessage;
            Seller = ctx.Seller;
            UserContext = ctx.UserContext;
        }

        /// <summary>
        ///     是否终止Invoke的执行。其结果是不再执行任何Plugin。
        /// </summary>
        public bool Aborted { get; set; }

        /// <summary>
        ///     如果Aborted为true，则响应此属性指定的消息
        /// </summary>
        public ResponseMessage Response { get; set; }
    }
}