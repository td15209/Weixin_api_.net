namespace Td.Weixin.Public.Common
{
    /// <summary>
    ///     获取微信公号关联的商家信息。
    ///     请继承自此类实现需要的方法。
    /// </summary>
    public abstract class SellerObtainer
    {
        /// <summary>
        ///     openid为商家id
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public abstract SellerContext Obtain(string openId);
    }
}