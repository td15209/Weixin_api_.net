/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 10:18:24
 *	Desc:	
 * 
*******************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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
            c.PageSize = 500;
            var list = c.ExecuteUserList();

            var failedCount = 0;
            var count = 0;
            list.ForEach(u =>
            {
                count++;
                if (count%10 == 0)
                {
                    //c = new Crawler(username, pwd);
                }
                var user = c.ExecuteUserInfo(u.id);
                if (user != null)
                {
                    Debug.WriteLine("user:{0}_{1}", user.NickName, user.FakeId);
                }
                else
                {
                    failedCount++;
                }

                if (failedCount > 2)
                {
                    Debug.WriteLine("错误了{0}次，执行了{1}", failedCount, count);
                    Assert.IsFalse(true);
                }
                    Thread.Sleep(1 * 1000);
            });

            //Assert.AreEqual("techdong", user.Username);
        }

        [TestMethod]
        public void SendMsg()
        {
            var list = new List<string>
            {
                "2080509240",//弟弟
                "1100424900", //我 
                "2740860201" //尹
            };


            var count = 0;
            while (count++ < 1)
            {
                var c = new Crawler(username, pwd);
                list.ForEach(s =>
                {
                    var r = c.SendTextMsg(s, string.Format("{0}sorry,just for test at {1}", 1, DateTime.Now));
                    Debug.WriteLine(r.msg);
                });
                Thread.Sleep(1 * 1000);
            }


            Debug.WriteLine("");
            //var r = c.SendTextMsg("1100424900", "sorry ,just for test");

            //Assert.IsTrue(r.IsSuccess);
            //Assert.AreEqual(10, count);
        }
    }
}