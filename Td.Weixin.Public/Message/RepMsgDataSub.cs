using System.Collections.Generic;
using System.Text;

namespace Td.Weixin.Public.Message
{
    /// <summary>
    ///     （响应）文本消息数据。
    ///     实际就是一个字符串
    /// </summary>
    public class TextMsgData : RepMsgData
    {
        [Output]
        public string Content { get; set; }

        public override string ToXmlText()
        {
            return MessageHelper.ToXmlText(this);
        }


        public static implicit operator TextMsgData(string s)
        {
            var ret = new TextMsgData
            {
                Content = s
            };
            return ret;
        }
    }

    /// <summary>
    ///     （响应）音乐消息数据
    /// </summary>
    public class MusicMsgData : RepMsgData
    {
        public const string NodeName = "Music";

        [Output]
        public string Title { get; set; }

        [Output]
        public string Description { get; set; }

        [Output]
        public string MusicUrl { get; set; }

        [Output]
        public string HQMusicUrl { get; set; }

        public override string ToXmlText()
        {
            var temp = MessageHelper.ToXmlText(this);
            return string.Format("<{0}>\n{1}\n</{0}>", NodeName, temp);
        }
    }

    /// <summary>
    ///     （响应）图文消息数据
    /// </summary>
    public class NewsMsgData : RepMsgData
    {
        public const string NodeName = "Articles";

        public NewsMsgData()
        {
            Items = new List<NewsItem>();
        }

        /// <summary>
        ///     具体条目列表。
        ///     相当于每一个条目就是条新闻
        /// </summary>
        public List<NewsItem> Items { get; set; }

        public int ArticleCount
        {
            get { return Items.Count; }
        }

        public override string ToXmlText()
        {
            var temp = new StringBuilder();
            foreach (var item in Items)
            {
                temp.AppendLine(item.ToXmlText());
            }
            var ret = string.Format("<{0}>\n{1}</{0}>", NodeName, temp);
            ret = string.Format("<ArticleCount>{0}</ArticleCount>\n{1}", ArticleCount, ret);
            return ret;
        }
    }

    public class NewsItem
    {
        public const string NodeName = "item";

        [Output]
        public string Title { get; set; }

        [Output]
        public string Description { get; set; }

        [Output]
        public string PicUrl { get; set; }

        [Output]
        public string Url { get; set; }

        public string ToXmlText()
        {
            var temp = MessageHelper.ToXmlText(this);
            return string.Format("<{0}>\n{1}\n</{0}>", NodeName, temp);
        }

        public override string ToString()
        {
            return ToXmlText();
        }
    }
}