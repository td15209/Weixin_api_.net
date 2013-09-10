/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-05 22:18:33
 *	Desc:	
 * 
*******************************/

using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void Test()
        {
            new Timer(state => { Debug.WriteLine("yes"); }, null, (5 - 3)/*避免时间误差*/ * 1000, Timeout.Infinite);
            Thread.Sleep(3 * 1000);


        }

    }
}