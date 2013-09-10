using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Td.Weixin.Public.Message;

namespace UnitTest
{
    [TestClass]
    public class MsgTest
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void ParseMessage_Success()
        {
            var text = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1351776360</CreateTime>
<MsgType><![CDATA[link]]></MsgType>
<Title><![CDATA[公众平台官网链接]]></Title>
<Description><![CDATA[公众平台官网链接]]></Description>
<Url><![CDATA[url]]></Url>
<MsgId>1234567890123456</MsgId>
</xml>";

            //ResponseMessage rep = new RepTextMessage();
            //rep.Content = "sd";
            //msg2.GetMusicResponse();
            //msg2.GetNewsResponse();
            //ReceiveMessage.ResisterHandler(handler);
            var msg2 = ReceiveMessage.ParseFromContext();
            ResponseMessage rep= msg2.Process();//switch来执行不同消息类型的方法
            //var rep = msg2.GetTextResponse();
            //rep.Data = (TextMsgData)"这是响应的文本内容";

            rep.Response();

            //Assert.AreEqual(MessageType.Link, msg.MsgType);
        }

        [TestMethod]
        public void CheckResponseXml()
        {
            var rep = new RepNewsMessage();
            var newsItems = new List<NewsItem>
            {
                new NewsItem()
                {
                    Description = "1",
                    PicUrl = "pic1",
                    Title = "t1",
                    Url = "url1"
                },
                new NewsItem()
                {
                    Description = "2",
                    PicUrl = "pic2",
                    Title = "t2",
                    Url = "url2"
                },
            };
            var data = new NewsMsgData();
            data.Items.AddRange(newsItems);
            rep.Data = data;

            var r = rep.ToXmlText();
            Debug.WriteLine(r);
        }
    }
}
