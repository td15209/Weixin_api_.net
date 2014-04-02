using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Td.Weixin.Public.Common;

namespace UnitTest
{
    [TestClass]
    public class CredentialTest
    {
        /*Modified:实际测试通过	By Dong[mailto:techdong@hotmail.com] 2013-09-05 16:09:58*/
        [TestMethod]
        public void AccessToken()
        {
            //从配置文件读取appid和appsecret
            var c = Credential.Create();

            //如果有其它需求，比如有多个公号，可以如此根据需要处理
            /* var c = new Credential()
             {
                 Appid = "yout appid",
                 Secret = "your secret"
             };*/

            for (var i = 0; i < 100; i++)
            {
                var beginTime = DateTime.Now.Ticks;
                var s = c.AccessToken;

                Debug.WriteLine("{0}，耗时{1}秒", s, (DateTime.Now.Ticks - beginTime) / 10000 / 1000);

                Assert.IsNotNull(s);
            }

        }

        /*Modified:多token缓存测试	By Dong[mailto:techdong@hotmail.com] 2013-09-06 13:02:14*/
        [TestMethod]
        public void MultiTokenCacheTest()
        {
            //从配置文件读取appid和appsecret
            var c = Credential.Create();

            var s = c.AccessToken;

            var s2 = c.AccessToken;

            Debug.WriteLine(s);

            Assert.AreEqual(s, s2);
        }
    }
}