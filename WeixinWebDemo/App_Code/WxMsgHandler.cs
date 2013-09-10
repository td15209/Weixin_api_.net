/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-05 22:18:33
 *	Desc:	
 * 
*******************************/

using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Web.Configuration;
using Td.Weixin.Public.Message;

/// <summary>
/// 消息处理
/// </summary>
public class WxMsgHandler : IMessageHandler
{
    public ResponseMessage OnTextMessage(RecTextMessage msg)
    {
        //文本响应
        var r = msg.GetTextResponse();
        r.Data = (TextMsgData)"this is response";

        //根据内容决定响应。此处为模拟
        switch (msg.Content)
        {
            case "image":
                return SimulateMusicMessage(msg);

            case "news":
                return SimulateNewsMessage(msg);
            
            case "event":
                r.Data = (TextMsgData) "恭喜你，你点中了菜单，要中奖了";
                break;

        }
        return r;
    }

    public ResponseMessage OnImageMessage(RecImageMessage msg)
    {
        var r = SimulateMusicMessage(msg);
        return r;
    }

    public ResponseMessage OnLinkMessage(RecLinkMessage msg)
    {
        var r = msg.GetNewsResponse();
        return r;
    }


    public ResponseMessage OnEventMessage(RecEventMessage msg)
    {
        var rep = msg.GetTextResponse();
        switch (msg.Event)
        {
            case RecEventMessage.Subscribe:
                rep.Data = (TextMsgData)"欢迎订阅！系统正在测试中，即将开放，敬请关注！";
                break;
        }
        return rep;
    }

    public ResponseMessage OnLocationMessage(RecLocationMessage msg)
    {
        var r = SimulateNewsMessage(msg);
        return r;
    }

    public void OnAfterMessage(ReceiveMessage msg,ResponseMessage repMsg)
    {
        LogHelper.Log(msg.ToString(),repMsg.ToString());
    }

    /********************   下面的代码仅仅是测试用了，实际并不需要 ********************/

    private static RepMusicMessage SimulateMusicMessage(ReceiveMessage msg)
    {
        //音乐响应
        var r = msg.GetMusicResponse();
        r.Data = new MusicMsgData
        {
            Description = "说明信息",
            Title = "听首歌吧",
            MusicUrl =WebConfigurationManager.AppSettings["music"],
            HQMusicUrl = WebConfigurationManager.AppSettings["hqmusic"]
        };
        return r;
    }

    private static RepNewsMessage SimulateNewsMessage(ReceiveMessage msg)
    {
        //图文响应
        var r = msg.GetNewsResponse();
        r.Data = new NewsMsgData
        {
            //响应3条新闻信息
            Items = new List<NewsItem>
            {
                new NewsItem
                {
                    Description = "new1",
                    Title = "老三发了",
                    PicUrl = "http://blog.tecd.pw/wp-content/uploads/2013/09/cropped-Desert.jpg",
                    Url = "http://blog.tecd.pw"
                },
                new NewsItem
                {
                    Description = "new2",
                    Title = "老大亏了",
                    PicUrl = "http://blog.tecd.pw/wp-content/uploads/2013/09/cropped-Desert.jpg",
                    Url = "http://blog.tecd.pw"
                },
                new NewsItem
                {
                    Description = "new2",
                    Title = "老二霉了",
                    PicUrl = "http://blog.tecd.pw/wp-content/uploads/2013/09/cropped-Desert.jpg",
                    Url = "http://blog.tecd.pw"
                }
            }
        };
        return r;
    }
    
}