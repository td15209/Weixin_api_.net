/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-04 13:29:33
 *	Desc:	
 * 
*******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;


namespace Td.Weixin.Public.Message
{
    /// <summary>
    /// 微信消息基类
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        [Output]
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        [Output]
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        [Output]
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [Output]
        public MessageType MsgType { get; protected set; }


        protected Message()
        {
            RootName = "xml";
        }

        /// <summary>
        /// 根节点名称。默认为xml
        /// </summary>
        public string RootName { get; set; }

        public override string ToString()
        {
            return ToXmlText();
        }

        /// <summary>
        /// 转换为最终xml文本
        /// </summary>
        /// <returns></returns>
        public virtual string ToXmlText()
        {
            return string.Format("<{0}>\n{1}\n</{0}>", RootName, InnerToXmlText());
        }

        /// <summary>
        /// 将内容转换为xml文本。最终xml为根节点包裹内容文本。
        /// </summary>
        /// <returns></returns>
        public virtual string InnerToXmlText()
        {
            return MessageHelper.ToXmlText(this, OutProperties);
        }

        /// <summary>
        /// 从xml文本中获取值
        /// </summary>
        /// <param name="text"></param>
        public virtual void ParseFrom(string text)
        {
            var root = XElement.Parse(text);
            foreach (var p in OutProperties)
            {
                var e = root.Element(XName.Get(p.Name));
                if (e == null)
                    continue;
                object value = e.Value;
                if (p.PropertyType == typeof(MessageType))
                {
                    value = Enum.Parse(typeof(MessageType), value.ToString(),true);
                }
                var tValue = Convert.ChangeType(value, p.PropertyType);
                p.SetValue(this, tValue, null);
            }
        }

        /// <summary>
        /// 获取需要序列化的属性列表
        /// </summary>
        protected virtual IEnumerable<PropertyInfo> OutProperties
        {
            get
            {
                return MessageHelper.GetOutputPropertyInfos(this);
            }
        }
    }

    /// <summary>
    /// 描述性数据，比如用于签名的数据
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MsgMetadata : Attribute
    {
        
    }

    /// <summary>
    /// 标记属性后，属性会被写入最终的xml文本中或从xml文本中读取值。
    /// 注意，Data属性不要标识，其始终回被写入结果串中。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OutputAttribute : Attribute
    {

    }

}