/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 10:18:24
 *	Desc:	
 * 
*******************************/

using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Td.Weixin.Public.Extra;

namespace UnitTest
{
    [TestClass]
    public class CrawlerTest
    {
        private string username = "chendong2qq.com@qq.com";
        private string pwd = "wobuzd";

        /// <summary>
        ///抓取用户列表
        /// </summary>
        [TestMethod]
        public void UserListTest()
        {
            var c = new Crawler(username, pwd);
            c.PageSize = 500;
            var list = c.ExecuteUserList();

            Assert.AreEqual(51, list.Count);
        }

        /// <summary>
        /// 抓取用户信息。包括组，备注
        /// </summary>
        [TestMethod]
        public void UserInfo()
        {
            var c = new Crawler(username, pwd);
            var user = c.ExecuteUserInfo("735376220");

            Assert.AreEqual("techdong", user.Username);
        }

        [TestMethod]
        public void SendMsg()
        {
            var c = new Crawler(username, pwd);
            var r = c.SendTextMsg("1100424900", "sorry ,just for test");
            
            Debug.WriteLine(r.msg);

            Assert.IsTrue(r.IsSuccess);
        }
    }
}