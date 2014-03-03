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
    public class QrCodeTest
    {
        [TestMethod]
        public void QrCreateTest()
        {
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }
            try
            {
                //生成带参数的二维码
                var ret = QrCodeManager.Default.Create(new QrCode
                {
                    action_name = QrCode.Temporary,
                    action_info = new QrCodeActionInfo() { scene = new QrCodeScene(){ scene_id = 200 } },
                    expire_seconds = 1800
                });

                Debug.WriteLine(ret.expire_seconds);
                Debug.WriteLine(ret.ticket);
            }
            catch (WxException ex)
            {
                Assert.Fail(ex.ErrMsg);
            }
        }

        [TestMethod]
        public void QrDownTest()
        {
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }
            try
            {
                QrCodeManager.Default.Exchange(
                    "gQE48DoAAAAAAAAAASxodHRwOi8vd2VpeGluLnFxLmNvbS9xLzUwekp5OG5sbk56bGFPSEtSR0tzAAIE81y1UgMECAcAAA==",//ticket
                    @"F:\移动硬盘\图片\qrcode.jpg");//保存路径
            }
            catch (WxException ex)
            {
                Assert.Fail(ex.ErrMsg);
            }
        }
    }
}
