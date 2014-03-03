/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-12-20 16:04:17
 *	Desc:	
 * 
*******************************/

using System;
using Newtonsoft.Json;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Extra.Models;

namespace Td.Weixin.Public.Extra
{
    /// <summary>
    ///     用于使用官方接口处理用户信息
    /// </summary>
    public class UserManager
    {
        public const string DefaultUserListUrl = "https://api.weixin.qq.com/cgi-bin/user/get";

        public const string DefaultUserInfoUrl = "https://api.weixin.qq.com/cgi-bin/user/info";

        public const string DefaultMoveUserUrl = "https://api.weixin.qq.com/cgi-bin/groups/members/update";

        public UserManager(string accessToken)
        {
            AccessToken = accessToken;
            UserListUrl = DefaultUserListUrl;
            UserInfoUrl = DefaultUserInfoUrl;
            UserUpdateUrl = DefaultMoveUserUrl;
        }

        /// <summary>
        ///     使用默认接口地址和缓存的accesstoken初始化实例.
        /// </summary>
        public static UserManager Default
        {
            get { return new UserManager(Credential.CachedAccessToken); }
        }

        public string AccessToken { get; set; }

        /// <summary>
        ///     关注者列表
        /// </summary>
        public string UserListUrl { get; set; }

        public string UserInfoUrl { get; set; }
        public string UserUpdateUrl { get; set; }

        /// <summary>
        ///     获取第一页（前10000用户）。
        ///     如果需要更多，请使用回调。
        /// </summary>
        /// <returns></returns>
        public WxUserListResult GetFirstPageUserList()
        {
            var s = new HttpHelper(UserListUrl).GetString(new FormData
            {
                {"access_token", AccessToken},
                {"next_openid", string.Empty}
            });

            var ret = JsonConvert.DeserializeObject<WxUserListResult>(s);
            if (ret.data == null)
                throw new WxException(JsonConvert.DeserializeObject<BasicResult>(s));

            return ret;
        }

        /// <summary>
        ///     分页获取用户列表。
        ///     注意：此方法是同步调用。
        /// </summary>
        /// <param name="callback">回调函数。参数1：用户列表； 参数2：第1开始的当前页索引</param>
        public void GetUserList(Action<WxUserListResult, int> callback)
        {
            var nextOpenid = string.Empty;
            const int pageSize = 10000; //
            var currentPage = 1;
            WxUserListResult ret;
            do
            {
                var s = new HttpHelper(UserListUrl).GetString(new FormData
                {
                    {"access_token", AccessToken},
                    {"next_openid", nextOpenid}
                });
                ret = JsonConvert.DeserializeObject<WxUserListResult>(s);

                if (ret.data == null)
                    throw new WxException(JsonConvert.DeserializeObject<BasicResult>(s));

                callback(ret, currentPage);

                currentPage++;
                nextOpenid = ret.next_openid;
            } while (ret.total > pageSize*currentPage);
        }

        /// <summary>
        ///     关注者基本信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public WxUserDetail GetUserInfo(string openid)
        {
            var s = new HttpHelper(UserInfoUrl).GetString(new FormData
            {
                {"access_token", AccessToken},
                {"openid", openid}
            });

            var ret = JsonConvert.DeserializeObject<WxUserDetail>(s);
            if (ret.openid == null)
                throw new WxException(JsonConvert.DeserializeObject<BasicResult>(s));

            return ret;
        }

        /// <summary>
        ///     将用户移动到指定分组
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public BasicResult MoveUserTo(string openid, int groupId)
        {
            var ao = new {openid, to_groupid = groupId};
            var s = new HttpHelper(UserUpdateUrl).PostString(JsonConvert.SerializeObject(ao), new FormData
            {
                {"access_token", AccessToken}
            });

            return JsonConvert.DeserializeObject<BasicResult>(s);
        }
    }
}