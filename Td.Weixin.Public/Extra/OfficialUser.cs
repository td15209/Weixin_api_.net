using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Td.Weixin.Public.Common;

namespace Td.Weixin.Public.Extra
{

    /// <summary>
    /// 关注者基本信息
    /// </summary>
    public class WxUserInfo
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public int subscribe { get; set; }

        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int sex { get; set; }

        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string headimgurl { get; set; }

        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public int subscribe_time { get; set; }

        #region 方法

        /// <summary>
        /// 使用UserManager.Default.MoveUserTo方法，将用户移到指定分组
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public BasicResult MoveTo(string groupId)
        {
            return UserManager.Default.MoveUserTo(openid, groupId);
        }
        #endregion
    }

    /// <summary>
    /// 用户列表结构
    /// </summary>
    public class WxUserListResult
    {
        public int total { get; set; }
        public int count { get; set; }
        public Data data { get; set; }
        public string next_openid { get; set; }
    }

    /// <summary>
    /// 用户列表openid列表结构
    /// </summary>
    public class Data
    {
        public string[] openid { get; set; }
    }
}
