using System.Net;
using Newtonsoft.Json;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Extra.Models;

namespace Td.Weixin.Public.Extra
{
    /// <summary>
    ///     用于处理二维码接口
    /// </summary>
    public class QrCodeManager
    {
        public const string DefaultCreateUrl = "https://api.weixin.qq.com/cgi-bin/qrcode/create";

        public const string DefaultDownloadUrl = "https://mp.weixin.qq.com/cgi-bin/showqrcode";

        public QrCodeManager(string accessToken)
        {
            AccessToken = accessToken;
            CreateUrl = DefaultCreateUrl;
            DownloadUrl = DefaultDownloadUrl;
        }

        /// <summary>
        ///     使用缓存的accesstoken创建实例
        /// </summary>
        public static QrCodeManager Default
        {
            get { return new QrCodeManager(Credential.CachedAccessToken); }
        }

        public string AccessToken { get; set; }
        public string CreateUrl { get; set; }
        public string DownloadUrl { get; set; }

        /// <summary>
        ///     创建二维码，获取二维码的ticket（用ticket可获取二维码的图）
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
        ///     用二维码ticket换取二维码图，保存到指定的位置。
        ///     下载失败反抛出WxException异常，异常码为http状态码。
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="filePath">接收图片的本地文件全路径</param>
        public void Exchange(string ticket, string filePath)
        {
            try
            {
                new WebClient().DownloadFile(string.Format("{0}?ticket={1}", DownloadUrl, ticket), filePath);
            }
            catch (WebException ex)
            {
                throw new WxException((int) ex.Status, ex.Message);
            }
        }
    }
}