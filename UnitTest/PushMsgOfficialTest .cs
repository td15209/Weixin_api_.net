using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Common;
using Td.Weixin.Public.Extra;
using Td.Weixin.Public.Extra.Models;

namespace UnitTest
{
    [TestClass]
    public class PushMsgTest
    {
        #region 结构测试

        [TestMethod]
        public void PushTextStruct()
        {
            var json = @"{
                    'touser':'OPENID',
                    'msgtype':'text',
                    'text':
                    {
                         'content':'Hello World'
                    }
                }";

            var ret = JsonConvert.DeserializeObject<TextMessageForPush>(json);

            Debug.WriteLine(ret);

            Assert.IsNotNull(ret);
        }

        [TestMethod]
        public void PushImageStruct()
        {
            var json = @"{
                        'touser':'OPENID',
                        'msgtype':'image',
                        'image':
                        {
                          'media_id':'MEDIA_ID'
                        }
                    }";

            var ret = JsonConvert.DeserializeObject<ImageMessageForPush>(json);

            Debug.WriteLine(ret);

            Assert.IsNotNull(ret);
        }


        [TestMethod]
        public void PushVoiceStruct()
        {
            var json = @"{
                        'touser':'OPENID',
                        'msgtype':'voice',
                        'voice':
                        {
                          'media_id':'MEDIA_ID'
                        }
                    }";

            var ret = JsonConvert.DeserializeObject<VoiceMessageForPush>(json);

            Debug.WriteLine(ret);

            Assert.IsNotNull(ret);
        }


        [TestMethod]
        public void PushVideoStruct()
        {
            var json = @"{
                        'touser':'OPENID',
                        'msgtype':'video',
                        'video':
                        {
                          'media_id':'MEDIA_ID',
                          'thumb_media_id':'THUMB_MEDIA_ID'
                        }
                    }";

            var ret = JsonConvert.DeserializeObject<VideoMessageForPush>(json);

            Debug.WriteLine(ret);

            Assert.IsNotNull(ret);
        }

        [TestMethod]
        public void PushMusicStruct()
        {
            var json = @"{
                        'touser':'OPENID',
                        'msgtype':'music',
                        'music':
                        {
                          'title':null,
                          'description':'MUSIC_DESCRIPTION',
                          'musicurl':'MUSIC_URL',
                          'hqmusicurl':'HQ_MUSIC_URL',
                          'thumb_media_id':'THUMB_MEDIA_ID' 
                        }
                    }";

            var ret = JsonConvert.DeserializeObject<MusicMessageForPush>(json);

            Debug.WriteLine(ret);

            Assert.IsNotNull(ret);
        }

        [TestMethod]
        public void PushNewsStruct()
        {
            var json = @"{
                        'touser':'OPENID',
                        'msgtype':'news',
                        'news':{
                            'articles': [
                             {
                                 'title':'Happy Day',
                                 'description':'Is Really A Happy Day',
                                 'url':'URL',
                                 'picurl':'PIC_URL'
                             },
                             {
                                 'title':'Happy Day',
                                 'description':'Is Really A Happy Day',
                                 'url':'URL',
                                 'picurl':'PIC_URL'
                             }
                             ]
                        }
                    }";

            var ret = JsonConvert.DeserializeObject<NewsMessageForPush>(json);

            Debug.WriteLine(ret);

            Assert.IsNotNull(ret);
        }

        #endregion

        [TestMethod]
        public void PushTextMsg()
        {
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }

            //文本消息
            var msg = new TextMessageForPush()
            {
                ToUser = "oaKZbtyB06l2QpTX6loCNYfoHD9A",
                Text = "你好"
            };
            var ret = MessagePusher.Default.Push(msg);

            Debug.WriteLine(ret.ErrMsg);

            Assert.IsTrue(ret.IsSuccess);
        }

        [TestMethod]
        public void PushImageMsg()
        {
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }

            //图片消息
            var msg = new ImageMessageForPush()
            {
                ToUser = "oaKZbtyB06l2QpTX6loCNYfoHD9A",
                Image = "mLtXk9ttRQaWBC0Cjzg24ZXtx0mcSInMcssLCWi2qbpUSuWbkeCYumwMkmZPtFz-" //可以直接设置图片id
            };
            var ret = MessagePusher.Default.Push(msg);

            Debug.WriteLine(ret.ErrMsg);

            Assert.IsTrue(ret.IsSuccess);
        }

        [TestMethod]
        public void PushNewsTest()
        {
            if (Credential.CachedAccessToken == null)
            {
                var t = Credential.Create().AccessToken;
            }

            //图片图文
            var news = new NewsMsgDataForPush();//定义图片内容
            news.Articles.AddRange(new List<NewsMsgDataItemForPush>
                {
                    new NewsMsgDataItemForPush()
                    {
                        Description = "这是描述",
                        PicUrl = "http://blog.tecd.pw/wp-content/uploads/http://bcs.duapp.com/td-bucket//blog/201309//QQ%E5%9B%BE%E7%89%8720130914160223.jpg",
                        Title = "这是标题",
                        Url = "http://blog.tecd.pw"
                    },
                    new NewsMsgDataItemForPush()
                    {
                        Description = "这是描述",
                        PicUrl = "http://blog.tecd.pw/wp-content/uploads/http://bcs.duapp.com/td-bucket//blog/201309//QQ%E5%9B%BE%E7%89%8720130914160223.jpg",
                        Title = "这是标题",
                        Url = "http://blog.tecd.pw"
                    },
                    new NewsMsgDataItemForPush()
                    {
                        Description = "这是描述",
                        PicUrl = "http://blog.tecd.pw/wp-content/uploads/http://bcs.duapp.com/td-bucket//blog/201309//QQ%E5%9B%BE%E7%89%8720130914160223.jpg",
                        Title = "这是标题",
                        Url = "http://blog.tecd.pw"
                    },
                });
            var msg = new NewsMessageForPush()
            {
                ToUser = "oaKZbtyB06l2QpTX6loCNYfoHD9A",
                News = news
            };
            var ret = MessagePusher.Default.Push(msg);

            Debug.WriteLine(ret.ErrMsg);

            Assert.IsTrue(ret.IsSuccess);

        }
    }
}
