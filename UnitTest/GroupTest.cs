using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Extra;
using Td.Weixin.Public.Extra.Models;

namespace UnitTest
{
    [TestClass]
    public class GroupTest
    {
        [TestMethod]
        public void CreateGroupTest()
        {
            try
            {
                if (string.IsNullOrEmpty(Credential.CachedAccessToken))
                {
                    var t = Credential.Create().AccessToken;
                }
                var r = GroupManager.Default.Create("我的分组");

                Debug.WriteLine(r.Group.ID);
                Debug.WriteLine(r.Group.Name);

            }
            catch (WxException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void QueryGroupTest()
        {
            try
            {
                if (string.IsNullOrEmpty(Credential.CachedAccessToken))
                {
                    var t = Credential.Create().AccessToken;
                }

                //获得所有分组
                var r = GroupManager.Default.Query();

                Debug.WriteLine("分组数：" + r.Groups.Count);
                foreach (var g in r.Groups)
                {
                    Debug.WriteLine(g.ID);
                    Debug.WriteLine(g.Name);
                }
            }
            catch (WxException ex)
            {
                Debug.WriteLine(ex.ErrMsg);

                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void UpdateGroupTest()
        {
            if (string.IsNullOrEmpty(Credential.CachedAccessToken))
            {
                var t = Credential.Create().AccessToken;
            }
            var r = GroupManager.Default.Update(new WxGroup { ID = 100, Name = "test-modified" });

            Debug.WriteLine(r.ErrMsg);

            Assert.IsTrue(r.IsSuccess);
        }
    }
}
