/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-04 22:51:46
 *	Desc:	
 * 
*******************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Td.Weixin.Public.Message
{
    public class MessageHelper
    {
        /// <summary>
        /// 将指定对象的带[Output]标识的属性序列化为xml文本
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToXmlText(object obj)
        {
            return ToXmlText(obj, GetOutputPropertyInfos(obj));
        }

        /// <summary>
        /// 将指定对象的指定属性列表序列化为xml文本。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static string ToXmlText(object obj, IEnumerable<PropertyInfo> ps)
        {
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Fragment,
                Indent = true,
                CloseOutput = false
            };
            var writer = XmlWriter.Create(sb, settings);
            foreach (var p in ps)
            {
                writer.WriteStartElement(p.Name);
                //字符串返回CDATA节点
                if (p.PropertyType == typeof(string))
                {
                    writer.WriteCData(Convert.ToString(p.GetValue(obj, null)));
                }
                else if (p.PropertyType == typeof(MessageType))
                {
                    writer.WriteCData(MessageTypeAttribute.ObtainMessageType((MessageType)p.GetValue(obj, null)));
                }
                else
                {
                    writer.WriteValue(p.GetValue(obj, null));
                }
                writer.WriteEndElement();
            }
            writer.Flush();
            writer.Close();
            return sb.ToString();
        }

        /// <summary>
        /// 获取指定对象的带[Output]标识的属性列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetOutputPropertyInfos(object obj)
        {
            var ps = obj.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(OutputAttribute), true).Any());
            return ps;
        }
    }
}