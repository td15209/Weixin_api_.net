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
            list.ForEach(u => Debug.Print("id:{0},nickname:{1},groupid:{2},remark:{3}\n", u.id, u.NickName, u.GroupID, u.RemarkName));

            Assert.IsTrue(list.Count > 0);
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
                if (count % 10 == 0)
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
        public void SendTextMsg()
        {
            var list = new List<string>
            {
                "2080509240",//弟弟
                "1100424900", //我 
                "1326394260", //晓亮 
                "2740860201" //尹
            };

            var start = DateTime.Now.Ticks;

            var count = 0;
            var c = new Crawler(username, pwd) { NoPreLoginCheck = true };
            while (count++ < 10)
            {
                list.ForEach(s =>
                {
                    var r = c.SendTextMsg(s, string.Format("{0}sorry,just for test at {1}", 1, DateTime.Now));
                    Debug.WriteLine(r.base_resp.err_msg);
                });
                Thread.Sleep(1 * 1);
            }


            Debug.WriteLine(count);

            var end = DateTime.Now.Ticks;
            Debug.Print("耗时：{0}秒", (end - start) / 10000 / 1000);

            //var r = c.SendTextMsg("1100424900", "sorry ,just for test");

            //Assert.IsTrue(r.IsSuccess);
            //Assert.AreEqual(10, count);
        }

        /// <summary>
        /// 官方群发，一天一条
        /// </summary>
        [TestMethod]
        public void SendToAllOverText()
        {
            var start = DateTime.Now.Ticks;

            var c = new Crawler(username, pwd) { NoPreLoginCheck = true };
            var r = c.SendToAllOverText(string.Format("小编在这里提前给大家拜年了，祝您(吸气)一往无前二龙腾飞三羊开泰四季安全五福临门六六大顺七星高照八方来财九九同心完美无瑕百事亨通千事吉祥万事满足!{0}", DateTime.Now));
            Debug.WriteLine(r.msg);

            Assert.IsTrue(r.IsSuccess);
        }



        [TestMethod]
        public void SendSingleNewsMsg()
        {
            var list = new List<string>
            {
                "2080509240",//弟弟
                "1100424900", //我 
                "1326394260", //晓亮 
                "2740860201" //尹
            };

            var start = DateTime.Now.Ticks;

            var count = 0;
            var c = new Crawler(username, pwd) { NoPreLoginCheck = true };
            while (count++ < 1)
            {
                list.ForEach(s =>
                {
                    var r = c.SendSingleNews(s);
                    Debug.WriteLine(r.base_resp.err_msg);
                });
                Thread.Sleep(1 * 1);
            }


            Debug.WriteLine(count);

            var end = DateTime.Now.Ticks;
            Debug.Print("耗时：{0}秒", (end - start) / 10000 / 1000);

            //var r = c.SendTextMsg("1100424900", "sorry ,just for test");

            //Assert.IsTrue(r.IsSuccess);
            //Assert.AreEqual(10, count);
        }

    }
}