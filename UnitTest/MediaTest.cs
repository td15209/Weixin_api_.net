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
                    MediaType = Media.Image,//媒体类型，此处为图片为例
                    MediaPath = @"F:\移动硬盘\图片\1-1.jpg" //本地图片路径
                });

                Debug.WriteLine(ret.MediaID, ret.Timestamp);

                Assert.IsTrue(ret.MediaID != null);
            }
            catch (WxMenuException ex)
            {
                Assert.Fail(ex.ErrMsg);

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
            //文件的保存路径。如果不确定以什么文件名保存，希望以默认文件名（媒体id为文件名）保存，可指定以反斜杠"\"结尾的文件名
            var directory = @"F:\移动硬盘\图片\images\f.jpg";

            var ret = MediaManager.Default.Download("mLtXk9ttRQaWBC0Cjzg24ZXtx0mcSInMcssLCWi2qbpUSuWbkeCYumwMkmZPtFz-", directory);

            Debug.WriteLine(ret.ErrMsg, ret.ErrCode);

            Assert.IsTrue(ret.IsSuccess);

        }
    }
}