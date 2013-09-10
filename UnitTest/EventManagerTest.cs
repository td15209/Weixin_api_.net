/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-05 22:18:33
 *	Desc:	
 * 
*******************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Event;
using Td.Weixin.Public.Message;

namespace UnitTest
{
    [TestClass]
    public class EventManagerTest
    {
        /// <summary>
        /// 事件注册（即绑定）
        /// </summary>
        [TestMethod]
        public void RegisterEvent()
        {
            EventManager.Register(RecEventMessage.Subscribe, args =>
            {
                Assert.IsNotNull(args.EventKey);
            });

            Assert.IsTrue(true);
        }

        /// <summary>
        /// 取消事件注册（即解除绑定）
        /// </summary>
        [TestMethod]
        public void UnRegisterEvent()
        {
            EventManager.UnRegister( args => { });

            Assert.IsTrue(true);
        }


    }
}