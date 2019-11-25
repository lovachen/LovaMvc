using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Oss.Models
{
    /// <summary>
    /// 图片信息
    /// </summary>
    [Serializable]
    public class ImageInfoModel
    {
        public string Bucket { get; set; }

        /// <summary>
        /// 图片文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 日期 ，也是存储目录日期 yyyy-mm-dd
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 存储子目录
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        /// 扩展名称
        /// </summary>
        public string ExtName { get; set; }
    }
}
