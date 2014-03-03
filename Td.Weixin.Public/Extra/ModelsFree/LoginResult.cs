/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 11:48:19
 *	Desc:	
 * 
*******************************/

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