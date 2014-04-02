namespace Td.Weixin.Public.Extra.ModelsFree
{
    /// <summary>
    ///     模拟群发消息时的响应结构
    /// </summary>
    internal class MsgSendCommonResult
    {
        public int ret { get; set; }

        public string msg { get; set; }

        public bool IsSuccess
        {
            get { return ret == 0; }
        }


        /// <summary>
        ///     为了返回结构的一致性，提供一个方法转换为普通结果结构
        /// </summary>
        /// <returns></returns>
        public CommonResult ToCommonResult()
        {
            return new CommonResult
            {
                ret = ret,
                err_msg = msg
            };
        }
    }

    /// <summary>
    ///     模拟单发消息时的响应结构
    /// </summary>
    internal class MsgSendResult
    {
        public CommonResult base_resp { get; set; }

        public bool IsSuccess
        {
            get { return base_resp != null && base_resp.IsSuccess; }
        }
    }
}