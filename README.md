Weinxin_api_.net
================

.Net访问微信公共平台接口

### 前言：
如有兴趣或疑问，请留言或入群交流，非常欢迎交流。 QQ群：289147891

### 环境：
* vs2012(没有任何补丁)
* .Net 4.0

***

### 添加依赖项
* json.net

***

### 使用演示
处理消息：
```C#
    var m = ReceiveMessage.ParseFromContext();
    
    //文本响应
    //var r = m.GetTextResponse();
    //r.Data = (TextMsgData)"this is response";

    //音乐响应
    /*var r = m.GetMusicResponse();
    r.Data=new MusicMsgData
    {
        Description = "desc",
        Title = "title",
        MusicUrl = "http://play.baidu.com/",
        HQMusicUrl = "hq url"
    };*/
    
    //图文响应
    var r = m.GetNewsResponse();
    r.Data=new NewsMsgData
    {
        //响应3条新闻信息
        Items = new List<NewsItem>
        {
            new NewsItem
            {
                Description = "new1",
                Title = "老三发了",
                PicUrl = "http://c.hiphotos.baidu.com/ting/pic/item/b8014a90f603738d538032bfb21bb051f919ec61.jpg",
                Url = "http://baidu.com"
            },
            new NewsItem
            {
                Description = "new2",
                Title = "老大亏了",
                PicUrl = "http://c.hiphotos.baidu.com/ting/pic/item/b8014a90f603738d538032bfb21bb051f919ec61.jpg",
                Url = "http://baidu.com"
            },
            new NewsItem
            {
                Description = "new2",
                Title = "老二霉了",
                PicUrl = "http://c.hiphotos.baidu.com/ting/pic/item/b8014a90f603738d538032bfb21bb051f919ec61.jpg",
                Url = "http://baidu.com"
            }
        }
    };
    r.Response();    
``` 

获取access_token
```C#
    var c = Credential.Create();
    string access_token=c.AccessToken;
```
添加菜单：
```C#
    var h = MenuHelper.Create();
    var json =
                @"{'button':[{'type':'click','name':'今日歌曲','key':'V1001_TODAY_MUSIC','sub_button':[]},
                {'type':'click','name':'歌手简介','key':'V1001_TODAY_SINGER','sub_button':[]},
                {'name':'菜单','sub_button':[{'type':'click','name':'hello word','key':'V1001_HELLO_WORLD','sub_button':[]},
                {'type':'click','name':'赞一下我们','key':'V1001_GOOD','sub_button':[]}]}]}"; 
    var menu = Menu.FromJson(json);
    var r = h.CreateMenu(menu);
```
获取菜单：
```C#
    var h = MenuHelper.Create();
    h.AccessToken = Credential.Create().AccessToken;
    var menu = h.QueryMenu();
```
删除菜单：
```C#
    var h = MenuHelper.Create();
    var r = h.DeleteMenu();
```
***
