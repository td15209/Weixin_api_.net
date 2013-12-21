/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-12-21 17:11:11
 *	Desc:	
 * 
*******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Td.Weixin.Public.Common;

namespace Td.Weixin.Public.Extra
{
    public class QrCodeManager
    {
        public const string DefaultCreateUrl = "https://api.weixin.qq.com/cgi-bin/qrcode/create";

        public const string DefaultDownloadUrl = "https://mp.weixin.qq.com/cgi-bin/showqrcode";

        /// <summary>
        /// 使用缓存的accesstoken创建实例
        /// </summary>
        public static QrCodeManager Default
        {
            get { return new QrCodeManager(Credential.CachedAccessToken); }
        }

        public string AccessToken { get; set; }
        public string CreateUrl { get; set; }
        public string DownloadUrl { get; set; }

        public QrCodeManager(string accessToken)
        {
            AccessToken = accessToken;
            CreateUrl = DefaultCreateUrl;
            DownloadUrl = DefaultDownloadUrl;
        }

        /// <summary>
        /// 创建二维码，获取二维码的ticket（用ticket可获取二维码的图）
        /// </summary>
        /// <param name="qrCode"></param>
        /// <returns></returns>
        public QrCodeResult Create(QrCode qrCode)
        {
            var s = new HttpHelper(CreateUrl).PostString(JsonConvert.SerializeObject(qrCode), new FormData
            {
                {"access_token", AccessToken}
            });

            var ret = JsonConvert.DeserializeObject<QrCodeResult>(s);

            if (string.IsNullOrEmpty(ret.ticket))
                throw new WxException(JsonConvert.DeserializeObject<BasicResult>(s));

            return ret;
        }

        /// <summary>
        /// 用二维码ticket换取二维码图，保存到指定的位置。
        /// 下载失败反抛出WxException异常，异常码为http状态码。
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="filePath">接收图片的本地文件全路径</param>
        public void Exchange(string ticket, string filePath)
        {
            try
            {
                new WebClient().DownloadFile(string.Format("{0}?ticket={1}", DownloadUrl,ticket), filePath);
            }
            catch (WebException ex)
            {
                throw new WxException((int)ex.Status, ex.Message);
            }
        }
    }


    #region 结构定义

    public class QrCode
    {
        /// <summary>
        /// 临时二维码。有过期时间，最大为1800秒，但能够生成较多数量
        /// </summary>
        public const string Temporary = "QR_SCENE";

        /// <summary>
        /// 永久的。数量较少（目前参数只支持1--1000）
        /// </summary>
        public const string Permanent = "QR_LIMIT_SCENE";

        /// <summary>
        /// 二维码类型，QR_SCENE为临时,QR_LIMIT_SCENE为永久。
        /// </summary>
        public string action_name { get; set; }

        /// <summary>
        /// 二维码详细信息
        /// </summary>
        public Action_Info action_info { get; set; }

        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过1800。
        /// 仅适用于临时二维码.
        /// </summary>
        public int expire_seconds { get; set; }
    }

    public class Action_Info
    {
        public Scene scene { get; set; }
    }

    public class Scene
    {
        /// <summary>
        /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为1000（目前参数只支持1--1000）
        /// </summary>
        public int scene_id { get; set; }
    }

    /// <summary>
    /// 获取二维码Ticket的响应结构。
    /// </summary>
    public class QrCodeResult
    {
        public string ticket { get; set; }
        public int expire_seconds { get; set; }
    }

    #endregion

}
