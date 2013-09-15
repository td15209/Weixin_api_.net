/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 11:48:19
 *	Desc:	
 * 
*******************************/

using System.Web.UI.WebControls;

namespace Td.Weixin.Public.Extra
{
    public class LoginRet
    {
        public int Ret { get; set; }

        /// <summary>
        /// 消息。成功后从此处取结果
        /// </summary>
        public string ErrMsg { get; set; }

        public int ShowVerifyCode { get; set; }

        public int ErrCode { get; set; }

        public bool IsSuccess { get { return ErrCode == 0; } }
    }
}