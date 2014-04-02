using System;
using System.Collections.Generic;
using System.Web.Configuration;

namespace Td.Weixin.Public.Common
{
    /// <summary>
    /// 凭据处理
    /// </summary>
    public class Credential
    {
        /// <summary>
        ///     获取access_token的接口地址
        /// </summary>
        public const string Url = "https://api.weixin.qq.com/cgi-bin/token";

        private static string _accessToken;

        /// <summary>
        ///     多access_token缓存（根据appid），满足一个服务器服务于多个微信公号的需求。
        /// </summary>
        private static readonly Dictionary<string, CredentialCache> MultiTokenCache =
            new Dictionary<string, CredentialCache>();

        /// <summary>
        ///     获取缓存的（最后一次获取的）AccessToken。
        ///     注意，即使缓存的token已过期或不存在也不会获取新AccessToken。
        /// </summary>
        public static string CachedAccessToken
        {
            get { return _accessToken; }
        }

        /// <summary>
        ///     获取access_token填写client_credential
        /// </summary>
        public string GrantType
        {
            get { return "client_credential"; }
        }

        /// <summary>
        ///     第三方用户唯一凭证
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        ///     第三方用户唯一凭证密钥，既appsecret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        ///     获取access_token。会缓存，过期后自动重新获取新的token。
        ///     <para>
        ///         <exception cref="WxException">获取access_token失败时抛出</exception>
        ///     </para>
        /// </summary>
        public string AccessToken
        {
            get
            {
                if (!MultiTokenCache.ContainsKey(Appid) || MultiTokenCache[Appid] == null
                    || MultiTokenCache[Appid].ExpireTime < DateTime.Now /*已过期*/)
                {
                    var helper = GetHelper();
                    var ret = helper.Get<CredentialResult>(new FormData
                    {
                        {"grant_type", GrantType},
                        {"appid", Appid},
                        {"secret", Secret},
                    });

                    //获取access_token失败
                    if (!ret.IsSuccess)
                        throw new WxException(ret.errcode, ret.errmsg);

                    _accessToken = ret.access_token;

                    MultiTokenCache[Appid] = new CredentialCache
                    {
                        AccessToken = ret.access_token,
                        ExpireTime = DateTime.Now.AddSeconds(ret.expires_in - 3 /*避免时间误差*/)
                    }; //缓存
                }

                _accessToken = MultiTokenCache[Appid].AccessToken; //2013-11-28 修复bug：Appid变化后，仍然获取上个成功的access_token

                return _accessToken;
            }
        }

        /// <summary>
        ///     移除缓存的token
        /// </summary>
        /// <param name="appId"></param>
        public static void RemoveCache(string appId)
        {
            MultiTokenCache.Remove(appId);
        }

        private HttpHelper GetHelper()
        {
            var ret = new HttpHelper(Url);
            return ret;
        }

        /// <summary>
        ///     获取配置文件中的凭证信息并填充到Credential实例
        /// </summary>
        /// <returns></returns>
        public static Credential Create()
        {
            var ret = new Credential
            {
                Appid = WebConfigurationManager.AppSettings["WeixinAppId"],
                Secret = WebConfigurationManager.AppSettings["WeixinSecret"]
            };
            return ret;
        }

        private class CredentialCache
        {
            public string AccessToken { get; set; }
            public DateTime ExpireTime { get; set; }
        }
    }

    /// <summary>
    ///     获取凭证时的响应数据
    /// </summary>
    internal class CredentialResult
    {
        /// <summary>
        ///     获取到的凭证
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        ///     凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        ///     错误信息号
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        ///     错误消息文本
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        ///     是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return !string.IsNullOrEmpty(access_token); }
        }
    }
}