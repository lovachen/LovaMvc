using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace cts.web.core.MediaItem
{
    /// <summary>
    /// 媒体存储
    /// </summary>
    public class MediaItemStorage : IMediaItemStorage
    {
        private IWebHelper _webHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webHelper"></param>
        public MediaItemStorage(IWebHelper webHelper)
        {
            _webHelper = webHelper;
        }

        /// <summary>
        /// 文件存储,返回路径的相对路径
        /// 存储到当前应用的目录下
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="virtualPath">虚拟相对目录 xxxx/xxx</param>
        /// <param name="fileName"></param>
        /// <returns>返回相对路径</returns>
        public string Storage(MemoryStream stream, string virtualPath, string fileName)
        {
            if (String.IsNullOrEmpty(virtualPath))
                throw new ArgumentException("virtualPath 不能为空");

            string psth = virtualPath.TrimSpace();

            if (psth.StartsWith('/'))
                throw new Exception($"{virtualPath} 不能以 '/' 开头");

            var physicalPath = _webHelper.MapPath(virtualPath);

            if (String.IsNullOrEmpty(physicalPath))
                throw new NoNullAllowedException("虚拟路径转换错误");

            if (!Directory.Exists(physicalPath))
                Directory.CreateDirectory(physicalPath);
            var fullPath = Path.Combine(physicalPath, fileName);

            using (var outStream = File.OpenWrite(fullPath))
            {
                stream.WriteTo(outStream);
                outStream.Flush();
                outStream.Close();
            }
            return Path.Combine(psth, fileName);
        }
    }
}
