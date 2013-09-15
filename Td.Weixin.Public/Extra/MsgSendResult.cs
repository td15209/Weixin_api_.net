/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 17:37:31
 *	Desc:	
 * 
*******************************/

namespace Td.Weixin.Public.Extra
{
    public class MsgSendResult
    {
        public int ret { get; set; }

        public string msg { get; set; }

        public bool IsSuccess { get { return ret == 0; } }
    }
}