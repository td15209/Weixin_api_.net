/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-04 22:34:32
 *	Desc:	
 * 
*******************************/

using System.Threading;
using System.Web.Configuration;

namespace Td.Weixin.Public.Common
{
    public class Credential
    {
        public const string Url = "https://api.weixin.qq.com/cgi-bin/token";
        private static string _accessToken;

        /// <summary>
        /// 获取缓存的（最后一次获取的）AccessToken。
        /// 注意，即使缓存的token已过期或不存在也不会获取新AccessToken。
        /// </summary>
        public static string CachedAccessToken
        {
            get
            {
                return _accessToken;
            }
        }

        /// <summary>
        /// 获取access_token填写client_credential
        /// </summary>
        public string GrantType
        {
            get
            {
                return "client_credential";
            }
        }

        /// <summary>
        /// 第三方用户唯一凭证
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 第三方用户唯一凭证密钥，既appsecret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 获取access_token。会缓存，过期后自动重新获取新的token。
        /// </summary>
        public string AccessToken
        {
            get
            {
                if (_accessToken == null)
                {
                    var helper = GetHelper();
                    var ret = helper.Get<Result>(new FormData
                    {
                       { "grant_type",GrantType},
                       { "appid",Appid},
                       { "secret",Secret},
                    });
                    _accessToken = ret.access_token;
                    new Timer(state => { _accessToken = null; }, null, ret.expires_in * 1000, Timeout.Infinite);
                }

                return _accessToken;
            }
        }

        private HttpHelper GetHelper()
        {
            var ret = new HttpHelper(Url);
            return ret;
        }

        /// <summary>
        /// 获取配置文件中的凭证信息并填充到Credential实例
        /// </summary>
        /// <returns></returns>
        public static Credential Create()
        {
            var ret = new Credential();
            ret.Appid = WebConfigurationManager.AppSettings["WeixinAppId"];
            ret.Secret = WebConfigurationManager.AppSettings["WeixinSecret"];
            return ret;
        }
    }

    /// <summary>
    /// 获取凭证时的响应数据
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 错误信息号
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 错误消息文本
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return !string.IsNullOrEmpty(access_token);
            }
        }
    }
}