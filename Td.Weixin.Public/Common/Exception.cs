using System;

namespace Td.Weixin.Public.Common
{
    public class WxException : ApplicationException
    {
        public WxException(int errCode, string errMsg)
        {
            ErrCode = errCode;
            ErrMsg = errMsg;
        }

        public WxException(BasicResult ret)
        {
            ErrCode = ret.ErrCode;
            ErrMsg = ret.ErrMsg;
        }

        public int ErrCode { get; private set; }

        public string ErrMsg { get; private set; }

        /// <summary>
        ///     获取描述当前异常的消息。
        /// </summary>
        /// <returns>
        ///     解释异常原因的错误消息或空字符串 ("")。
        /// </returns>
        public override string Message
        {
            get { return ErrMsg; }
        }
    }
}