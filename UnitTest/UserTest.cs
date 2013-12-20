using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Extra;

namespace UnitTest
{
    [TestClass]
    public class UserTest
    {
        private string _openid = "oaKZbtyB06l2QpTX6loCNYfoHD9A";

        [TestMethod]
        public void UserListTest()
        {
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }
            try
            {
                //获取第一页（第1W）用户列表
                var ret = UserManager.Default.GetFirstPageUserList();

                Debug.WriteLine("总数：" + ret.total);
                foreach (var s in ret.data.openid)
                {
                    Debug.WriteLine(s);
                }
            }
            catch (WxException ex)
            {
                Assert.Fail(ex.ErrMsg);
            }
        }

        [TestMethod]
        public void UserPagedListTest()
        {
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }
            try
            {
                var callback = new Action<WxUserListResult, int>((ret, currentPage) =>
                {
                    Debug.WriteLine("总数：" + ret.total + ",当前页：" + currentPage);
                    foreach (var s in ret.data.openid)
                    {
                        Debug.WriteLine(s);
                    }
                });
                //分页获取，每页10000
                UserManager.Default.GetUserList(callback);
            }
            catch (WxException ex)
            {
                Assert.Fail(ex.ErrMsg);
            }
        }

        [TestMethod]
        public void UserInfoTest()
        {
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }
            try
            {
                //获取用户基本信息
                var ret = UserManager.Default.GetUserInfo(_openid);

                Debug.WriteLine(ret.openid + ret.nickname);
            }
            catch (WxException ex)
            {
                Assert.Fail(ex.ErrMsg);
            }
        }

        [TestMethod]
        public void UserMoveTo()
        {
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }

            //移动用户到组
           // var ret = UserManager.Default.MoveUserTo(_openid, 101);

            //如果已经获取到用户，可以以另一种方式移动
            var user = new WxUserInfo() {openid = _openid};//模拟获取一个用户
            var ret = user.MoveTo(101);

            Debug.WriteLine(ret.ErrMsg);

            Assert.IsTrue(ret.IsSuccess);
        }

    }
}
