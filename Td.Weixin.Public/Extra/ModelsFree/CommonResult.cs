/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	$date$
 *	Desc:	
 * 
*******************************/

namespace Td.Weixin.Public.Extra.ModelsFree
{
    public class CommonResult
    {
        public int ret { get; set; }

        public string err_msg { get; set; }

        public bool IsSuccess
        {
            get { return ret == 0; }
        }
    }
}