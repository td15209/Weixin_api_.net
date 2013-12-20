/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	$date$
 *	Desc:	
 * 
*******************************/

using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Extra;

namespace UnitTest
{
    [TestClass]
    public class MediaTest
    {
        [TestMethod]
        public void UpdateMediaTest()
        {
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }
            try
            {
                //上传
                var ret = MediaManager.Default.Upload(new LocalMedia
                {
                    MediaType = Media.Image,
                    MediaPath = @"F:\移动硬盘\图片\1-1.jpg" //@"F:\移动硬盘\我的文档\美图图库\示例图片_02.jpg"
                });

                Debug.WriteLine(ret.MediaID, ret.Timestamp);

                Assert.IsTrue(ret.MediaID != null);
            }
            catch (WxMenuException ex)
            {
                Debug.WriteLine(ex.ErrMsg);

            }
        }

        [TestMethod]
        public void DownloadMediaTest()
        {
            //mLtXk9ttRQaWBC0Cjzg24ZXtx0mcSInMcssLCWi2qbpUSuWbkeCYumwMkmZPtFz-
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }
            
            //下载
            var directory = @"F:\移动硬盘\图片\images\f.jpg";
            var ret = MediaManager.Default.Download("mLtXk9ttRQaWBC0Cjzg24ZXtx0mcSInMcssLCWi2qbpUSuWbkeCYumwMkmZPtFz-", directory);

            Debug.WriteLine(ret.ErrMsg, ret.ErrCode);

            Assert.IsTrue(ret.IsSuccess);

        }
    }
}