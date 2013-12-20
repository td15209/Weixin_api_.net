/*******************************
 *	Author:	Dong[http://blog.tecd.pw] [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	$date$
 *	Desc:	
 * 
*******************************/

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Td.Weixin.Public.Common;

namespace Td.Weixin.Public.Extra
{
    public class MediaManager
    {
        /// <summary>
        /// 当前的接口地址
        /// </summary>
        public const string DefaultUploadUrl = "http://file.api.weixin.qq.com/cgi-bin/media/upload";

        //
        public const string DefaultDownloadUrl = "http://file.api.weixin.qq.com/cgi-bin/media/get";

        /// <summary>
        /// 使用缓存的accessToken与默认地址初始化实例
        /// </summary>
        public static MediaManager Default
        {
            get { return new MediaManager(Credential.CachedAccessToken); }
        }

        public string AccessToken { get; set; }

        public string UploadUrl { get; set; }

        public string DownLoadUrl { get; set; }

        public MediaManager(string accessToken)
        {
            AccessToken = accessToken;
            UploadUrl = DefaultUploadUrl;
            DownLoadUrl = DefaultDownloadUrl;
        }

        /// <summary>
        /// 上传媒体。
        /// 注意：如果上传失败，会抛出WxMenuException异常
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        public RemoteMedia Upload(LocalMedia media)
        {
            var checkRet = MediaChecker(media);
            if (!checkRet.IsSuccess)
                throw new WxException(-9999, checkRet.ErrMsg);//todo:考虑是否设计新的Exception类，以区分异常是由微信抛出还是本地抛出

            var param = new FormData()
            {
                {"access_token", AccessToken},
                {"type", media.MediaType}
            };
            var rs = new HttpHelper(UploadUrl).Upload(param, media.MediaPath);
            var ret = JsonConvert.DeserializeObject<RemoteMedia>(rs);
            if (string.IsNullOrEmpty(ret.MediaID))
            {
                var ex = JsonConvert.DeserializeObject<BasicResult>(rs);
                throw new WxException(ex);
            }
            return ret;
        }

        /// <summary>
        /// 检测上传的媒体是否满足基本要求
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        private BasicResult MediaChecker(LocalMedia media)
        {
            var sizes = new Dictionary<string, long>
            {
                {Media.Image,128*1024},
                {Media.Voice,256*1024},
                {Media.Video,1*1024*1024},
                {Media.Thumb,64*1024},
            };

            if (media == null)
                return BasicResult.GetFailed("缺少媒体参数");
            if (!File.Exists(media.MediaPath))
                return BasicResult.GetFailed("指定的媒体文件不存在");
            if (string.IsNullOrEmpty(media.MediaType))
                return BasicResult.GetFailed("未指定媒体类型");
            if (new FileInfo(media.MediaPath).Length > sizes[media.MediaType])
                return BasicResult.GetFailed(string.Format("指定的媒体文件超过限制大小{0}K", sizes[media.MediaPath] / 1024));

            return BasicResult.GetSuccess();
        }

        /// <summary>
        /// 下载媒体文件。
        /// 如果下载成功，返回值的ErrMsg属性携带保存的文件全路径
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="filePath">
        ///     保存下载的文件的本地文件名或目录(以\结尾)。
        ///     如果为目录，则文件名为微信响应的文件名。
        ///     如果文件已经存在，则会覆盖原文件。
        /// </param>
        /// <returns>如果成功，ErrMsg属性携带保存的文件全路径</returns>
        public BasicResult Download(string mediaId, string filePath)
        {
            var param = new FormData
            {
                {"access_token", AccessToken},
                {"media_id", mediaId}
            };

            var url = string.Format("{0}?{1}", DownLoadUrl, param.Format());
            var request = HttpHelper.CreateRequest(url);
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                var disposition = response.Headers["Content-disposition"];
                if (string.IsNullOrEmpty(disposition))
                {
                    var s = new StreamReader(stream, Encoding.UTF8).ReadToEnd();
                    return JsonConvert.DeserializeObject<BasicResult>(s);
                }

                filePath = filePath.EndsWith(@"\")
                    ? string.Format("{0}{1}", filePath, Regex.Match(disposition, "(?<=filename\\s*=\\s*\\\"?)[^\\\"]+", RegexOptions.IgnoreCase))
                    : filePath;
                var fs = new FileStream(filePath, FileMode.OpenOrCreate);
                try
                {
                    var buffer = new byte[128 * 1024]; //128K
                    int i;
                    while ((i = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, i);
                    }

                    return BasicResult.GetSuccess(filePath);
                }
                finally
                {
                    fs.Close();
                }
            }
        }
    }

    public class Media
    {
        /// <summary>
        /// 图片（image）: 128K，支持JPG格式
        /// </summary>
        public const string Image = "image";

        /// <summary>
        /// 语音（voice）：256K，播放长度不超过60s，支持AMR\MP3格式
        /// </summary>
        public const string Voice = "voice";

        /// <summary>
        /// 视频（video）：1MB，支持MP4格式
        /// </summary>
        public const string Video = "video";

        /// <summary>
        /// 缩略图（thumb）：64KB，支持JPG格式
        /// </summary>
        public const string Thumb = "thumb";

        /// <summary>
        /// 常量中描述的媒体类型
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string MediaType { get; set; }

    }


    /// <summary>
    /// 本地媒体
    /// </summary>
    public class LocalMedia : Media
    {

        /// <summary>
        /// 本地全路径
        /// </summary>
        public string MediaPath { get; set; }
    }

    /// <summary>
    /// 远程媒体。指保存在微信服务器上的媒体。
    /// </summary>
    public class RemoteMedia : Media
    {
        /// <summary>
        /// 媒体文件上传后，获取时的唯一标识
        /// </summary>
        [JsonProperty(PropertyName = "media_id")]
        public string MediaID { get; set; }

        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public int Timestamp { get; set; }
    }
}