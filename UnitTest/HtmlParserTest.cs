/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-05 22:18:33
 *	Desc:	
 * 
*******************************/

using System;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Text;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Td.Weixin.Public.Common;

namespace UnitTest
{
    [TestClass]
    public class HtmlParserTest
    {
        [TestMethod]
        public void Parse()
        {
            var sourceUrl = "http://ccwb.yunnan.cn/html/2013-09/09/node_202.htm";
            var s = HttpHelper.GetStream(sourceUrl);
            var doc = new HtmlDocument();
            doc.Load(s,Encoding.UTF8);
            var ele = doc.DocumentNode.SelectSingleNode("//div[@id='layer43']");
            var alinks = ele.SelectNodes(".//a");

            Debug.WriteLine("a标签数据：" + alinks.Count);
            foreach (var alink in alinks)
            {
                var link = alink.Attributes["href"].Value;
                Debug.WriteLine("a {0}:{1}",alink.InnerText,new Uri(new Uri(sourceUrl),link));
            }

            Assert.IsNotNull(ele);
        }

    }
}