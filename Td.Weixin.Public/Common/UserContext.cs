/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-05 22:18:33
 *	Desc:	
 * 
*******************************/

using System.Collections.Generic;

namespace Td.Weixin.Public.Common
{
    /// <summary>
    /// 以appid和用户id为key，处理用户上下文信息。可用来处理如回复数字返回数字对应的内容
    /// </summary>
    public static class UserContext
    {
        private static readonly Dictionary<string, UserContextData> Datas = new Dictionary<string, UserContextData>();

        /// <summary>
        /// 生成字典的key值
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        private static string KeyCreator(string appid, string userid)
        {
            return string.Format("{0}{1}", appid, userid);
        }

        /// <summary>
        /// 添加或更新用户关联信息。
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userid"></param>
        /// <param name="data"></param>
        public static void Set(string appid, string userid, UserContextData data)
        {
            Datas.Add(KeyCreator(appid, userid), data);
        }


        /// <summary>
        /// 获取用户关联信息
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static UserContextData Get(string appid, string userid)
        {
            var key = KeyCreator(appid, userid);
            return Datas.ContainsKey(key) ? Datas[key] : null;
        }
    }
}