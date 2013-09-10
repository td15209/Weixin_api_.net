/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-04 22:34:44
 *	Desc:	
 * 
*******************************/

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Td.Weixin.Public.Common
{
    public class MenuHelper
    {
        public static string CreateUrlDefault = "https://api.weixin.qq.com/cgi-bin/menu/create";

        /// <summary>
        /// 查询菜单接口地址
        /// </summary>
        public static string QueryUrlDefault = "https://api.weixin.qq.com/cgi-bin/menu/get";

        /// <summary>
        /// 删除菜单接口地址
        /// </summary>
        public static string DeleteUrlDefault = "https://api.weixin.qq.com/cgi-bin/menu/delete";

        public string AccessToken { get; set; }

        /// <summary>
        /// 获取默认的MenuHelper,此实例设置了默认Url并读取缓存的access_token
        /// </summary>
        /// <returns></returns>
        public static MenuHelper Create()
        {
            var ret = new MenuHelper
            {
                AccessToken = Credential.CachedAccessToken,
                CreateUrl = CreateUrlDefault,
                QueryUrl = QueryUrlDefault,
                DeleteUrl = DeleteUrlDefault
            };
            return ret;
        }

        public string CreateUrl { get; set; }
        public string QueryUrl { get; set; }
        public string DeleteUrl { get; set; }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public MenuResult CreateMenu(Menu menu)
        {
            var hh = new HttpHelper(CreateUrl);
            var r = hh.Post<MenuResult>(menu.ToString(), new FormData { { "access_token", AccessToken } });

            return r;
        }

        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <returns></returns>
        public Menu QueryMenu()
        {
            var hh = new HttpHelper(QueryUrl);
            var oo = new { menu = new Menu() };
            var or = hh.GetAnonymous(new FormData { { "access_token", AccessToken } }, oo);
            var r = or.menu;
            return r;
        }

        /// <summary>
        /// 取消当前使用的自定义菜单
        /// </summary>
        public MenuResult DeleteMenu()
        {
            var hh = new HttpHelper(DeleteUrl);
            var r = hh.Get<MenuResult>(new FormData { { "access_token", AccessToken } });
            return r;
        }
    }

    /// <summary>
    /// 菜单结构
    /// </summary>
    public class Menu
    {
        [JsonProperty(PropertyName = "button")]
        public List<MenuItem> Items { get; set; }

        public static Menu FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Menu>(json);
        }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            return JsonConvert.SerializeObject(this, settings);
        }
    }

    /// <summary>
    /// 表示一个button
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 用户点击click类型按钮后，微信服务器会通过消息接口(event类型)推送点击事件给开发者，
        /// 并且带上按钮中开发者填写的key值，开发者可以通过自定义的key值进行消息回复
        /// </summary>
        public const string Click = "click";

        /// <summary>
        /// 用户点击view类型按钮后，会直接跳转到开发者指定的url中
        /// </summary>
        public const string View = "view";

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 类型。从常量中获取
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// view button 的url地址
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "sub_button")]
        public List<MenuItem> Items { get; set; }
    }

    public class MenuResult
    {
        // ReSharper disable once InconsistentNaming
        public int errcode { get; set; }

        // ReSharper disable once InconsistentNaming
        public string errmsg { get; set; }

        public bool IsSuccess
        {
            get { return 0 == errcode; }
        }
    }
}