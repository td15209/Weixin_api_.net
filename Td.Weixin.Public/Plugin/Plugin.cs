using Td.Weixin.Public.Message;

namespace Td.Weixin.Public.Plugin
{
    /// <summary>
    ///     插件基类。所有插件必须继承自此类。
    /// </summary>
    public abstract class Plugin
    {
        /// <summary>
        ///     由此判断，决定是否处理当前请求。
        ///     <para>比如，如果不同的商家可能启用了不同的插件，可在此判断，当商家没有启动此插件时返回false即可。</para>
        ///     <para>如果商家没有启用此插件但需要给予提示，使得用户催促商家开通此插件的话，可返回true然后在Execute方法下返回提示信息。</para>
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public virtual bool CanProcess(PluginContext ctx)
        {
            return false; //不处理任何请求
        }

        public virtual ResponseMessage Execute(PluginContext ctx)
        {
            return null; //不会有任何响应。实际上，不会执行到此方法。
        }
    }
}