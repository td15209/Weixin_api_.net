/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 10:26:00
 *	Desc:	
 * 
*******************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.XPath;
using HtmlAgilityPack;
using Jurassic.Library;

namespace Td.Weixin.Public.Extra
{
    /// <summary>
    /// 爬虫，抓取用户信息。
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
        /// 发发送消息。
        /// </summary>
        public const string DefaultSendMsg = "https://mp.weixin.qq.com/cgi-bin/singlesend?t=ajax-response&lang=zh_CN";

        /// <summary>
        /// 获取头像地址。从此地址请求的响应就是图片。
        /// </summary>
        public const string DefaultHeadImgUrl = "https://mp.weixin.qq.com/cgi-bin/getheadimg?";
        //token=1582431588&fakeid=2561215522

        /// <summary>
        /// 文本消息
        /// </summary>
        public const int TextMsg = 1;


        /// <summary>
        /// 图文消息
        /// </summary>
        public const int NewsMsg = 10;

        private bool _logined;
        private readonly string _userName;
        private readonly string _pwd;
        private readonly CookieContainer _cc;
        private string _token;


        public int PageSize = 500;

        public int GroupId = 0;

        public Encoding Encoding = Encoding.UTF8;

        public string Language = "zh_CN";

        public Crawler(string userName, string pwd)
        {
            _userName = userName;
            _pwd = pwd;
            _cc = new CookieContainer();
        }

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

        public LoginRet Login()
        {
            //用于登录要发送的数据
            var dic = new Dictionary<string, object>
            {
                {"username", _userName},
                {"pwd", Md5(_pwd)},
                {"imgcode", string.Empty},
                {"f", "json"}
            };
            var ret = HttpExtendedHelper.Post<LoginRet>(DefaultLoginUrl, dic, _cc, DefaultLoginReferUrl)
                ?? new LoginRet() { ErrCode = -9999, ErrMsg = "超时而未收到任何服务器响应" };
            if (ret.IsSuccess)
            {
                _token = Regex.Match(ret.ErrMsg, @"(?<=token=)\d+").Value;
            }
            else
            {
                ret.ErrMsg = MapErrCode(ret.ErrCode);
            }
            return ret;
        }

        public List<WxUserFree> ExecuteUserList()
        {
            PreLogin();

            var ret = new List<WxUserFree>();
            var index = 0;
            ResultPerPageFree r;
            do
            {
                r = InnerUserList(_token, PageSize, index++);
                ret.AddRange(r.FriendsList);
            } while (r.PageIndex <= r.PageCount);

            return ret;
        }

        private void PreLogin()
        {
            //如果已登陆，探测是否已过期
            if (_logined)
            {
                var url = string.Format("https://mp.weixin.qq.com/cgi-bin/home?t=home/index&token={0}&lang=zh_CN", _token);
                //todo:_logined = HttpHelper.HeadHttpCode(url, null, _cc, null) == HttpStatusCode.OK;
            }

            if (!_logined)
            {
                _logined = Login().IsSuccess;
            }

            if (!_logined)
            {
                throw new AuthenticationException("登陆失败，请检查用户名及密码后重试");
            }
        }

        private ResultPerPageFree InnerUserList(string token, int pageSize, int pageIndex)
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
            };
            //var s = HttpHelper.GetString(DefaultUserListUrl, dic, _cc, null);
            var stream = HttpExtendedHelper.Get(DefaultUserListUrl, dic, _cc, null);
            var doc = new HtmlDocument();
            doc.Load(stream, Encoding);
            var ss = doc.DocumentNode.SelectNodes("//script");
            var se = new Jurassic.ScriptEngine();
            foreach (var s in ss.Where(e => e.GetAttributeValue("src", string.Empty).Equals(string.Empty)))
            {
                try
                {
                    se.Execute("if(typeof(wx)=='undefined') wx={}");
                    se.Execute(s.InnerText);
                }
                catch
                {
                    // se.Execute("if(!wxwx.cgiData)delete wx");
                }
            }
            var temp = se.GetGlobalValue("wx") as Jurassic.Library.ObjectInstance;
            var r = ResultPerPageFree.FromObjectInstance(temp.GetPropertyValue("cgiData") as ObjectInstance);
            return r;
        }

        public WxUserInfoFree ExecuteUserInfo(string fakeid)
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
            var p = "t=user/index&pagesize=10&pageidx=0&type=0&groupid=100&token={0}&lang={1}";
            refer = string.Format(refer, _token, Language);
            return HttpExtendedHelper.Post<WxUserInfoRespFree>(DefaultUserInfoUrl, dic, _cc, refer).contact_info;
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
                default:
                    i = "未知的返回。";
                    break;
            }
            return i;
        }

        #endregion

        public MsgSendResult SendTextMsg(string fakeid, string msg)
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
                /*{"fid", 10000003},//后3个文本消息不需要
                {"fileid", 10000000},
                {"appmsgid", 10000003}*/
            };
            var r = HttpExtendedHelper.Post<MsgSendResult>(DefaultSendMsg, dic, _cc, refer)
                ?? new MsgSendResult() { ret = -9999, msg = "未收到微信服务器响应" };
            return r;
        }
    }
}