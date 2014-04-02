namespace Td.Weixin.Public.Extra.Models
{
    public class QrCode
    {
        /// <summary>
        ///     临时二维码。有过期时间，最大为1800秒，但能够生成较多数量
        /// </summary>
        public const string Temporary = "QR_SCENE";

        /// <summary>
        ///     永久的。数量较少（目前参数只支持1--1000）
        /// </summary>
        public const string Permanent = "QR_LIMIT_SCENE";

        /// <summary>
        ///     二维码类型，QR_SCENE为临时,QR_LIMIT_SCENE为永久。
        /// </summary>
        public string action_name { get; set; }

        /// <summary>
        ///     二维码详细信息
        /// </summary>
        public QrCodeActionInfo action_info { get; set; }

        /// <summary>
        ///     该二维码有效时间，以秒为单位。 最大不超过1800。
        ///     仅适用于临时二维码.
        /// </summary>
        public int expire_seconds { get; set; }
    }

    public class QrCodeActionInfo
    {
        public QrCodeScene scene { get; set; }
    }

    public class QrCodeScene
    {
        /// <summary>
        ///     场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为1000（目前参数只支持1--1000）
        /// </summary>
        public int scene_id { get; set; }
    }

    /// <summary>
    ///     获取二维码Ticket的响应结构。
    /// </summary>
    public class QrCodeResult
    {
        public string ticket { get; set; }
        public int expire_seconds { get; set; }
    }
}