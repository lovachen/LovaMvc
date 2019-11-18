using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cts.web.core.MediaItem
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMediaItemStorage
    {
        /// <summary>
        /// 文件存储,返回路径的相对路径
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="virtualPath">虚拟相对目录 xxxx/xxx</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string Storage(MemoryStream stream, string virtualPath, string fileName);
    }
}
