using cts.web.core.Skia;
using Lova.Core.MediaItem;
using Lova.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Oss.Controllers
{
    public class ImageCnController : AreaOssController
    {
        private IWebHostEnvironment _webHostEnvironment;
        private MarkLogoService _markLogoService;
        private BucketCutService _bucketCutService;
        private BucketService _bucketService;

        public ImageCnController(IWebHostEnvironment webHostEnvironment,
            MarkLogoService markLogoService,
            BucketCutService bucketCutService,
            BucketService bucketService)
        {
            _bucketService = bucketService;
            _bucketCutService = bucketCutService;
            _markLogoService = markLogoService;
            _webHostEnvironment = webHostEnvironment;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="name"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("{bucket}/{mod}/{crc32}/{name}")]
        public IActionResult Get(string bucket, string mod, string crc32, string name)
        {
            string abPath = System.IO.Path.Combine(MediaItemConfig.RootDir, bucket, mod, crc32, name);
            if (!System.IO.File.Exists(abPath))
            {
                return NotFile();
            }
            using (FileStream fs = new FileStream(abPath, FileMode.Open))
            {
                byte[] bt = new byte[fs.Length];
                fs.Read(bt, 0, bt.Length);
                string ext = Path.GetExtension(name) ?? ".jpg";

                return File(bt, $"image/{ext.Substring(1)}");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="mod"></param>
        /// <param name="crc32"></param>
        /// <param name="name"></param>
        /// <param name="query">参数，以'逗号分隔',例如:m_fill,w_200xh_300,l_logo</param>
        /// <returns></returns>
        [Route("{bucket}/{mod}/{crc32}/{name}!{query}")]

        public IActionResult Get(string bucket, string mod, string crc32, string name, string query)
        {
            string abPath = System.IO.Path.Combine(MediaItemConfig.RootDir, bucket.ToLower(), mod, crc32, name.ToLower());
            if (!System.IO.File.Exists(abPath))
            {
                return NotFile();
            }
            string ext = Path.GetExtension(name) ?? ".jpg";

            string cut = null, resize = null;
            Stream stream = null;

            if (!String.IsNullOrEmpty(query))
            {
                if (!_bucketCutService.ValueExists(bucket, query))
                {
                    return NotFile();
                }

                string thum_AbPath = System.IO.Path.Combine(MediaItemConfig.RootDir, bucket.ToLower(), mod, crc32, name.ToLower() + "!" + query.ToLower());
                if (!System.IO.File.Exists(thum_AbPath))
                {
                    var arr = query.Split(',');
                    foreach (var q in arr)
                    {
                        string temp = q.ToLower();
                        if (temp.StartsWith("m_"))
                            cut = temp;
                        if (temp.StartsWith("w_") || temp.StartsWith("h_"))
                            resize = temp;
                        if (temp.Equals("l_logo"))
                        {
                            stream = _markLogoService.GetStream();
                        }
                    }
                    using (var img = SkiaHelper.MakeThumb(abPath, cut, resize, ext, stream))
                    {
                        var bt = img.ToArray();

                        using (FileStream fs = System.IO.File.OpenWrite(thum_AbPath))
                        {
                            fs.Write(bt, 0, bt.Length);
                        }
                        return File(bt, $"image/{ext.Substring(1)}");
                    }
                }
                else
                {
                    abPath = thum_AbPath;
                }
            }
            using (FileStream fs = new FileStream(abPath, FileMode.Open))
            {
                byte[] bt = new byte[fs.Length];
                fs.Read(bt, 0, bt.Length);
                return File(bt, $"image/{ext.Substring(1)}");
            }
        }






        #region 私有方法
        /// <summary>
        /// 没有文件时返回
        /// </summary>
        /// <returns></returns>
        private IActionResult NotFile()
        {
            var bt = _markLogoService.Get404Stream();
            if (bt == null)
                return NotFound();
            return File(bt, "image/png");
        }

        #endregion

    }
}
