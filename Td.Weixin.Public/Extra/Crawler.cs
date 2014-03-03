/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 10:26:00
 *	Desc:	模拟操作的爬虫
 * 
*******************************/

using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Extra.ModelsFree;

namespace Td.Weixin.Public.Extra
{
    /// <summary>
    ///     爬虫，抓取用户信息。
    /// </summary>
    public class Crawler
    {
        public const string DefaultUserListUrl = "https://mp.weixin.qq.com/cgi-bin/contactmanage";
        public const string DefaultLoginReferUrl = "https://mp.weixin.qq.com/";
        public const string DefaultLoginUrl = "http://mp.weixin.qq.com/cgi-bin/login?lang=zh_CN";
        public const string DefaultUserInfoUrl = "https://mp.weixin.qq.com/cgi-bin/getcontactinfo";

        /*
            type:10 //图文消息
            content:
            error:false
            fid:10000003
            fileid:10000000
            imgcode:
            tofakeid:1100424900
            token:1582431588
            ajax:1
            appmsgid:10000003
         */

        /// <summary>
        ///     发发送消息。
        /// </summary>
        public const string DefaultSendMsg = "https://mp.weixin.qq.com/cgi-bin/singlesend";


        //
        public const string DefaultSendToAll = "https://mp.weixin.qq.com/cgi-bin/masssend";

        /// <summary>
        ///     获取头像地址。从此地址请求的响应就是图片。
        /// </summary>
        public const string DefaultHeadImgUrl = "https://mp.weixin.qq.com/cgi-bin/getheadimg?";

        //token=1582431588&fakeid=2561215522

        /// <summary>
        ///     文本消息
        /// </summary>
        public const int TextMsg = 1;


        /// <summary>
        ///     图文消息
        /// </summary>
        public const int NewsMsg = 10;


        private readonly CookieContainer _cc;
        private readonly string _pwd;
        private readonly string _userName;

        public Encoding Encoding = Encoding.UTF8;

        /// <summary>
        ///     欲获取用户列表的组id
        /// </summary>
        public int GroupId = 0;

        public string Language = "zh_CN";
        public int PageSize = 500;

        /// <summary>
        ///     是否已第一次登陆
        /// </summary>
        private bool _inited;

        private bool _logined;
        private string _token;

        public Crawler(string userName, string pwd)
        {
            NoPreLoginCheck = true;
            _userName = userName;
            _pwd = pwd;
            _cc = new CookieContainer();
        }

        /// <summary>
        ///     发送前不要检查是否已登陆，直接发送。默认为true
        /// </summary>
        public bool NoPreLoginCheck { get; set; }

        public string Md5(string input, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding;
            var bs = encoding.GetBytes(input);
            var rbs = new MD5CryptoServiceProvider().ComputeHash(bs, 0, bs.Length);
            var sb = new StringBuilder();
            foreach (var b in rbs)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString().ToLower();
        }

        public LoginResult Login()
        {
            //用于登录要发送的数据
            var dic = new Dictionary<string, object>
            {
                {"username", _userName},
                {"pwd", Md5(_pwd)},
                {"imgcode", string.Empty},
                {"f", "json"}
            };
            var ret = HttpHelper.Post<LoginResult>(DefaultLoginUrl, dic, _cc, DefaultLoginReferUrl)
                      ?? new LoginResult {ErrCode = -9999, ErrMsg = "超时而未收到任何服务器响应"};
            if (ret.IsSuccess)
            {
                _token = Regex.Match(ret.ErrMsg, @"(?<=token=)\d+").Value;
                _inited = true; //已登陆过
            }
            else
            {
                ret.ErrMsg = MapErrCode(ret.ErrCode);
            }
            return ret;
        }


        private void PreLogin()
        {
            if (NoPreLoginCheck && _inited)
                return;


            //如果已登陆，探测是否已过期
            if (_logined)
            {
                //var url = string.Format("https://mp.weixin.qq.com/cgi-bin/home?t=home/index&token={0}&lang=zh_CN", _token);
                //todo:_logined = HttpHelper.HeadHttpCode(url, null, _cc, null) == HttpStatusCode.OK;
            }

            var ret = new LoginResult();
            if (!_logined)
            {
                ret = Login();
                _logined = ret.IsSuccess;
            }

            if (!_logined)
            {
                throw new AuthenticationException(ret.ErrMsg);
            }
        }

        /// <summary>
        ///     获取指定组下的关注用户列表。数量由PageSize属性指定。
        ///     如果想获取所有用户，将PageSize属性设置得足够大即可。
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<WxUserInfo> ExecuteUserList(int groupId = 0)
        {
            PreLogin();

            GroupId = groupId;
            var o = new {contacts = new List<WxUserInfo>()};
            var json = InnerUserList(_token, PageSize, 0).contact_list;
            var ret = JsonConvert.DeserializeAnonymousType(json, o).contacts;

            return ret;
        }

        /// <summary>
        ///     获取所有组信息
        /// </summary>
        /// <returns></returns>
        public List<WxGroup> ExecuteGroups()
        {
            PreLogin();

            return JsonConvert.DeserializeObject<WxUserDetailResultGroup>(InnerUserList(_token, 1, 0).group_list).groups;
        }

        private UserInfoJsonListResult InnerUserList(string token, int pageSize, int pageIndex)
        {
            var dic = new Dictionary<string, object>
            {
                {"t", "user/index"},
                {"pagesize", pageSize},
                {"pageidx", pageIndex},
                {"type", "0"},
                {"groupid", GroupId},
                {"token", token},
                {"lang", Language},
                {"f", "json"},
            };
            var r = HttpHelper.Get<UserInfoJsonListResult>(DefaultUserListUrl, dic, _cc, null);
            return r;
        }


        /// <summary>
        ///     获取用户详细信息
        /// </summary>
        /// <param name="fakeid"></param>
        /// <returns></returns>
        public WxUserDetail ExecuteUserInfo(string fakeid)
        {
            PreLogin();

            var dic = new Dictionary<string, object>
            {
                {"token", _token},
                {"lang", Language},
                {"t", "ajax-getcontactinfo"},
                {"fakeid", fakeid}
            };
            //X-Requested-With:XMLHttpRequest
            var refer = "https://mp.weixin.qq.com/cgi-bin/contactmanage?";
            refer = string.Format(refer, _token, Language);
            var wxUserDetailResult = HttpHelper.Post<WxUserDetailResult>(DefaultUserInfoUrl, dic, _cc, refer);
            return wxUserDetailResult.contact_info;
        }

        public CommonResult SendTextMsg(string fakeid, string msg)
        {
            PreLogin();

            //refer是必须的，否则错误： need post
            var refer =
                string.Format(
                    "https://mp.weixin.qq.com/cgi-bin/singlemsgpage?fromfakeid={0}&count=20&t=wxm-singlechat&token={1}&lang=zh_CN",
                    fakeid, _token);
            var dic = new Dictionary<string, object>
            {
                {"type", TextMsg},
                {"content", msg},
                {"error", "false"},
                {"imgcode", ""},
                {"tofakeid", fakeid},
                {"token", _token},
                {"ajax", 1},
                {"lang", Language},
                {"t", "ajax-response"}
            };
            var r = GetResponse(DefaultSendMsg, dic, refer);
            return r.base_resp;
        }


        /// <summary>
        ///     todo:由于发送的是在微信后台预定义的消息，不实用。待寻方案
        /// </summary>
        /// <param name="fakeid"></param>
        /// <param name="news"></param>
        /// <returns></returns>
        public CommonResult SendSingleNews(string fakeid, object news = null)
        {
            //refer是必须的，否则错误： need post
            var refer =
                string.Format(
                    "https://mp.weixin.qq.com/cgi-bin/singlemsgpage?fromfakeid={0}&count=20&t=wxm-singlechat&token={1}&lang=zh_CN",
                    fakeid, _token);
            var dic = new Dictionary<string, object>
            {
                {"type", NewsMsg},
                {"app_id", 10015263}, //图片id。应该是对应于文本消息的 content
                //{"error", "false"},
                {"appmsgid", 10015263}, //与图片id相同
                {"imgcode", ""},
                {"tofakeid", fakeid},
                {"token", _token},
                //{"random", new Random().Next(0,1)},
                {"ajax", 1},
                {"f", "json"},
                {"lang", Language},
                {"t", "ajax-response"}
            };
            var r = GetResponse(DefaultSendMsg, dic, refer);
            return r.base_resp;
        }

        /// <summary>
        ///     官方群发。一个公众号一天只能发一条。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public CommonResult SendToAllOverText(string text)
        {
            PreLogin();

            //refer是必须的，否则错误： need post
            var refer = string.Format("https://mp.weixin.qq.com/cgi-bin/masssendpage?t=mass/send&token={0}&lang=zh_CN",
                _token);
            var dic = new Dictionary<string, object>
            {
                {"type", TextMsg},
                {"content", text},
                {"imgcode", ""},
                {"token", _token},
                {"sex", 1}, //0:所有； 1:男； 2:女
                {"groupid", -1}, //-1为全部组
                {"country", ""}, //空串为全部。值为文字，如"中国"
                {"province", ""}, //空串为全部。值为文字，如"云南"
                {"city", ""}, //空串为全部。值为文字，如"昆明"
                {"synctxweibo", 0}, //
                {"synctxnews", 0}, //
                {"ajax", 1},
                {"lang", Language},
                {"f", "json"},
                {"t", "ajax-response"}
            };
            var r = GetResponse2(DefaultSendToAll, dic, refer);
            return r.ToCommonResult();
        }

        private MsgSendResult GetResponse(string url, Dictionary<string, object> dic, string refer)
        {
            return HttpHelper.Post<MsgSendResult>(url, dic, _cc, refer)
                   ?? new MsgSendResult {base_resp = new CommonResult {ret = -9999, err_msg = "未收到微信服务器响应"}};
        }

        private MsgSendCommonResult GetResponse2(string url, Dictionary<string, object> dic, string refer)
        {
            return HttpHelper.Post<MsgSendCommonResult>(url, dic, _cc, refer)
                   ?? new MsgSendCommonResult {ret = -9999, msg = "未收到微信服务器响应"};
        }

        #region

        private string MapErrCode(int errCode)
        {
            string i;
            switch (errCode)
            {
                case -1:
                    i = "系统错误，请稍候再试。";
                    break;
                case -2:
                    i = "帐号或密码错误。";
                    break;
                case -3:
                    i = "您输入的帐号或者密码不正确，请重新输入。";
                    break;
                case -4:
                    i = "不存在该帐户。";
                    break;
                case -5:
                    i = "您目前处于访问受限状态。";
                    break;
                case -6:
                    i = "请输入图中的验证码";
                    break;
                case -7:
                    i = "此帐号已绑定私人微信号，不可用于公众平台登录。";
                    break;
                case -8:
                    i = "邮箱已存在。";
                    break;
                case -32:
                    i = "您输入的验证码不正确，请重新输入";
                    break;
                case -200:
                    i = "因频繁提交虚假资料，该帐号被拒绝登录。";
                    break;
                case -94:
                    i = "请使用邮箱登陆。";
                    break;
                case 10:
                    i = "该公众会议号已经过期，无法再登录使用。";
                    break;
                case 65201:
                case 65202:
                    i = "成功登陆，正在跳转...";
                    break;
                case 0:
                    i = "成功登陆，正在跳转...";
                    break;
                case -100:
                    i = "海外帐号请在公众平台海外版登录,<a href=\"http://admin.wechat.com/\">点击登录</a>";
                    break;

                case -9999:
                    i = "请求超时";
                    break;
                default:
                    i = "未知的返回。";
                    break;
            }
            return i;
        }

        #endregion
    }
}