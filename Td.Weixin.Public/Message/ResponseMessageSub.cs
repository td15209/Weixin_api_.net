using System.CodeDom;

namespace Td.Weixin.Public.Message
{
    /// <summary>
    ///     （响应）文件消息
    /// </summary>
    public class RepTextMessage : ResponseMessage
    {
        public RepTextMessage()
        {
            MsgType = MessageType.Text;
        }
    }

    /// <summary>
    ///     （响应）音乐消息
    /// </summary>
    public class RepMusicMessage : ResponseMessage
    {
        public RepMusicMessage()
        {
            MsgType = MessageType.Music;
        }
    }

    /// <summary>
    ///     （响应）图文消息
    /// </summary>
    public class RepNewsMessage : ResponseMessage
    {
        public RepNewsMessage()
        {
            MsgType = MessageType.News;
        }
    }

    /// <summary>
    /// （响应）客服消息
    /// </summary>
    public class TranserCustomerService : ResponseMessage
    {
        public TranserCustomerService()
        {
            MsgType = MessageType.TransferCustomerSerivce;
        }
    }
}