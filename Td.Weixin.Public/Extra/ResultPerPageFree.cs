/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-15 12:08:23
 *	Desc:	
 * 
*******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jurassic.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Td.Weixin.Public.Extra
{
    public class ResultPerPageFree
    {
        [JsonProperty(PropertyName = "pageIdx")]
        public int PageIndex { get; set; }

        [JsonProperty(PropertyName = "pageCount")]
        public int PageCount { get; set; }

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "groupsList")]
        public List<WxGroup> GroupsList { get; set; }

        [JsonProperty(PropertyName = "friendsList")]
        public List<WxUserFree> FriendsList { get; set; }

        [JsonProperty(PropertyName = "currentGroupId")]
        public int CurrentGroupId { get; set; }

        public static ResultPerPageFree FromObjectInstance(Jurassic.Library.ObjectInstance instance)
        {
            /*为了简单，省去1万地反射...*/
            var ret = new ResultPerPageFree();
            foreach (var pi in ret.GetType().GetProperties())
            {
                try
                {
                    var value = instance.GetPropertyValue(JsonPropertyName(pi));
                    if (pi.Name == "GroupsList")
                    {
                        var ls = value as ArrayInstance;
                        if (ls != null)
                        {
                            var wxGroups = (from ObjectInstance o in ls.ElementValues select FromObject<WxGroup>(o)).ToList();
                            pi.SetValue(ret, wxGroups, null);
                        }
                    }
                    else if (pi.Name == "FriendsList")
                    {
                        var ls = value as ArrayInstance;
                        if (ls != null)
                        {
                            var wxGroups = (from ObjectInstance o in ls.ElementValues select FromObject<WxUserFree>(o)).ToList();
                            pi.SetValue(ret, wxGroups, null);
                        }
                    }
                    else
                    {
                        pi.SetValue(ret, Convert.ChangeType(value, pi.PropertyType), null);
                    }
                }
                catch { }
            }
            return ret;
        }

        public static T FromObject<T>(Jurassic.Library.ObjectInstance instance) where T : new()
        {
            if (instance == null)
                return default(T);

            var ret = new T();
            foreach (var pi in ret.GetType().GetProperties())
            {
                try
                {
                    var value = instance.GetPropertyValue(JsonPropertyName(pi));
                    if (pi.PropertyType.IsPrimitive || pi.PropertyType == typeof(string))
                    {
                        pi.SetValue(ret, Convert.ChangeType(value, pi.PropertyType), null);
                    }
                }
                catch { }
            }
            return ret;
        }

        public static string JsonPropertyName(PropertyInfo pi)
        {
            var a = pi.GetCustomAttributes(typeof(JsonPropertyAttribute), true).FirstOrDefault() as JsonPropertyAttribute;
            return a == null ? pi.Name : a.PropertyName;
        }
    }
}