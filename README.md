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
* json.net

***
```ObjectiveC 
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
        MusicUrl = "http://play.baidu.com/?__m=mboxCtrl.playSong&__a=331525&__o=/song/331525||playBtn&fr=ps||www.baidu.com#loaded",
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
