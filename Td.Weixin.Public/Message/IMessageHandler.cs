/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-05 22:18:33
 *	Desc:	
 * 
*******************************/

namespace Td.Weixin.Public.Message
{
    public interface IMessageHandler
    {
        /// <summary>
        /// 收到文本消息时执行
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        ResponseMessage OnTextMessage(RecTextMessage msg);

        /// <summary>
        /// 收到图片消息时执行
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        ResponseMessage OnImageMessage(RecImageMessage msg);

        /// <summary>
        /// 收到链接消息时执行
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        ResponseMessage OnLinkMessage(RecLinkMessage msg);

        /// <summary>
        /// 收到地理位置消息时执行
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        ResponseMessage OnLocationMessage(RecLocationMessage msg);

        /// <summary>
        /// 收到事件消息时执行
        /// </summary>
        /// <param name="msg"></param> 
        /// <returns></returns>
        ResponseMessage OnEventMessage(RecEventMessage msg);

/* 
        /// <summary>
        /// 处理消息前执行。可用来重组收到消息等
        /// </summary>
        /// <param name="msg"></param>
        void OnBeforeMessage(ReceiveMessage msg);
*/

        /// <summary>
        /// 处理完消息后执行。可用来处理日志等
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="repMsg"></param>
        void OnAfterMessage(ReceiveMessage msg, ResponseMessage repMsg);
    }
}