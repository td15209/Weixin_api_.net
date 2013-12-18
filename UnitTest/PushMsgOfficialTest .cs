using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Td.Weixin.Public.Extra;

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

            var ret = JsonConvert.DeserializeObject<PushTextMessage>(json);

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

            var ret = JsonConvert.DeserializeObject<PushImageMessage>(json);

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

            var ret = JsonConvert.DeserializeObject<PushVoiceMessage>(json);

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

            var ret = JsonConvert.DeserializeObject<PushVideoMessage>(json);

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

            var ret = JsonConvert.DeserializeObject<PushMusicMessage>(json);

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

            var ret = JsonConvert.DeserializeObject<PushNewsMessage>(json);

            Debug.WriteLine(ret);

            Assert.IsNotNull(ret);
        }

        #endregion

        [TestMethod]
        public void PushTextMsg()
        {
            var msg = new PushTextMessage()
            {
                ToUser = "123",
                Text = "你好"
            };

            var ret = MessagePusher.Default.Push(msg);

            Debug.WriteLine(ret.ErrMsg);

            Assert.IsTrue(ret.IsSuccess);
        }
    }
}
