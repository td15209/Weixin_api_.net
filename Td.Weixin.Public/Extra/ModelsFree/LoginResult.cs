namespace Td.Weixin.Public.Extra.ModelsFree
{
    /// <summary>
    ///     模拟登陆的响应结构
    /// </summary>
    public class LoginResult
    {
        public int Ret { get; set; }

        /// <summary>
        ///     消息。成功后从此处取结果
        /// </summary>
        public string ErrMsg { get; set; }

        public int ShowVerifyCode { get; set; }

        public int ErrCode { get; set; }

        public bool IsSuccess
        {
            get { return ErrCode == 0; }
        }
    }
}