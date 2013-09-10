/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流
 *	Date:	2013-09-04 22:34:38
 *	Desc:	
 * 
*******************************/

using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Td.Weixin.Public.Common;

namespace UnitTest
{
    [TestClass]
    public class MenuTest
    {
        [TestMethod]
        public void CreateMenu()
        {
            var h = MenuHelper.Create();
            h.AccessToken = Credential.Create().AccessToken;
            var json =
                @"{'button':[{'name':'了解','sub_button':[{'name':'关于我们','type':'view','url':'http://www.shhot.net/wx/about.html','sub_button':[]},
{'name':'联系我们','type':'view','url':'http://www.shhot.net/wx/contact.html','sub_button':[]}]},
                {'name':'服务','sub_button':[{'name':'网站建设','type':'view','url':'http://www.shhot.net/wx/wangzhan.html','sub_button':[]},
{'name':'企业微官网','type':'view','url':'http://www.shhot.net/wx/wgw.html','sub_button':[]},
{'name':'企业微商城','type':'view','url':'http://www.shhot.net/wx/wsc.html','sub_button':[]}]},{'name':'文档','sub_button':[{'name':'公司新闻','type':'view','url':'http://www.shhot.net/wx/newslist.html','sub_button':[]},{'name':'技术文档','type':'view','url':'http://www.shhot.net/wx/jishulist.html',
'sub_button':[]},{'name':'作品展示','type':'view','url':'http://www.shhot.net/wx/caselist.html','sub_button':[]}]}]}
            "; 
            var menu = Menu.FromJson(json);
            var r = h.CreateMenu(menu);

            Debug.WriteLine(r.errmsg);
            Debug.WriteLine(menu);

            Assert.IsTrue(r.IsSuccess);
        }

        /*Modified:实际测试通过	By Dong[mailto:techdong@hotmail.com] 2013-09-05 16:25:25*/
        [TestMethod]
        public void QueryMenu()
        {
            var h = MenuHelper.Create();
            h.AccessToken = Credential.Create().AccessToken;
            var menu = h.QueryMenu();

            Debug.WriteLine(menu);
            Assert.IsNotNull(menu);
        }

        [TestMethod]
        public void DeleteMenu()
        {
            var h = MenuHelper.Create();
            var r = h.DeleteMenu();

            Assert.IsTrue(r.IsSuccess);
        }
    }
}