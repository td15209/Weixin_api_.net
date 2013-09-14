/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-05 22:18:33
 *	Desc:	
 * 
*******************************/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Microsoft.SqlServer.Server;
using Td.Weixin.Public.Message;

namespace Td.Weixin.Public.Common
{
    /// <summary>
    /// 以微信公众号toOpenID和用户id为key，处理用户上下文信息。可用来处理如回复数字返回数字对应的内容
    /// </summary>
    public partial class UserContext
    {

        #region 静态成员

        /// <summary>
        /// 生成字典的key值
        /// </summary>
        /// <param name="publicOpenid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        private static string KeyCreator(string publicOpenid, string userid)
        {
            return string.Format("{0}{1}", publicOpenid, userid);
        }

        /// <summary>
        /// 用户上下文信息缓存超时时间（分钟）
        /// </summary>
        public static int Timeout = 5;

        /// <summary>
        /// 添加或更新用户关联信息。
        /// <para>添加的信息会在超时后自动移除。超时时间为滑动时间。</para>
        /// </summary>
        /// <param name="toOpenID">微信公众号openid</param>
        /// <param name="userid"></param>
        /// <param name="data"></param>
        public static void Set(string toOpenID, string userid, UserContext data)
        {
            var key = KeyCreator(toOpenID, userid);
            HttpRuntime.Cache.Insert(key, data, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, Timeout, 0));
        }


        /// <summary>
        /// 获取用户关联信息
        /// </summary>
        /// <param name="toOpenID">微信公众号openid</param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static UserContext Get(string toOpenID, string userid)
        {
            var key = KeyCreator(toOpenID, userid);
            return HttpRuntime.Cache.Get(key) as UserContext;
        }

        /// <summary>
        /// 如果希望主动移除缓存存，可使用此方法。
        /// </summary>
        /// <param name="toOpenId"></param>
        /// <param name="userId"></param>
        public static void Remove(string toOpenId, string userId)
        {
            var key = KeyCreator(toOpenId, userId);
            HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// 本次请求的用户上下文
        /// </summary>
        public static UserContext Current
        {
            get
            {
                var msg = ReceiveMessage.ParseFromContext();
                if (msg == null) return null;
                return Get(msg.ToUserName, msg.FromUserName);
            }
        }

        #endregion


        public object Tag { get; set; }
    }
}