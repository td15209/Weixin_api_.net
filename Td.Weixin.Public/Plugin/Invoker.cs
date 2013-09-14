/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-13 21:10:36
 *	Desc:	
 * 
*******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Message;

namespace Td.Weixin.Public.Plugin
{
    /// <summary>
    /// 调用者，控制插件的执行。
    /// </summary>
    public class Invoker
    {
        private static readonly List<Plugin> Plugins = new List<Plugin>();

        /// <summary>
        /// 是否验证签名信息。
        /// 如果为true，签名失败时当不会执行插件.
        /// </summary>
        public static bool RequireCheck = true;

        /// <summary>
        /// 商家信息获取者。用于根据商家微信公号的openid获取商家信息。
        /// 如果平台有多个微信公号，必须实现此抽象类，否则不能获取到商家信息。
        /// </summary>
        public static SellerObtainer Obtainer { get; set; }

        /// <summary>
        /// 注册插件。
        /// <para>注册的插件则按顺序执行，直到找到第一个接受调用的Plugin。这意味着顺序很重要。</para>
        /// <para>同时，最后一个插件应该能处理所有调用，否则本次执行将无任何结果。</para>
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="index"></param>
        public static void RegisterPlugin(Plugin plugin, int? index = null)
        {
            if (plugin == null)
                throw new ArgumentNullException("plugin", "不能注册null插件");

            if (index.HasValue)
            {
                Plugins.Insert(index.Value, plugin);
            }
            else
            {
                Plugins.Add(plugin);
            }
        }

        /// <summary>
        /// 将调用转到合适的插件并由找到的插件执行处理。
        /// <para>如果没有任何插件接收此次调用，将返回null。</para>
        /// </summary>
        /// <param name="context">如果为null，则自动从请求中获取数据</param>
        /// <returns></returns>
        public static ResponseMessage Invoke(InvokerContext context = null)
        {
            if (context == null)
            {
                context = ParseFromContext();
            }
            var msg = context.Message;//接收的消息
            var ctx = new PluginContext()
            {
                ReceiveMessage = msg,
                Seller = Obtainer == null ? null : Obtainer.Obtain(msg.ToUserName),
                UserContext = UserContext.Get(msg.ToUserName, msg.FromUserName)
            };

            //需要验证签名
            if (RequireCheck && !EntrySign.ParseFromContext().Check(ctx.Seller == null ? null : ctx.Seller.Token))
            {
                return null;
            }

            var plugin = Plugins.FirstOrDefault(p => p.CanProcess(ctx));
            if (plugin == null) return null;

            var rep = plugin.Execute(ctx);//传递PluginContext
            return rep;
        }

        /// <summary>
        /// 从请求中获取上下文信息。
        /// 如果有扩展，请同时扩展此方法。
        /// </summary>
        /// <returns></returns>
        private static InvokerContext ParseFromContext()
        {
            return new InvokerContext
            {
                Message = ReceiveMessage.ParseFromContext()
            };
        }
    }
}