using cts.web.core.Librs;
using cts.web.core.MediaItem;
using Lova.Core.MediaItem;
using Lova.Mvc.Areas.Oss.Models;
using Lova.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Oss.Controllers
{
    public class UploadController : AreaOssController
    {
        private SettingService _settingService;
        private BucketService _bucketService;
        private BucketImageService _bucketImageService;
        private IWebHostEnvironment _hostingEnvironment;
        private IMediaItemStorage _mediaItemStorage;

        public UploadController(SettingService settingService,
            BucketImageService bucketImageService,
            BucketService bucketService,
            IWebHostEnvironment hostingEnvironment,
            IMediaItemStorage mediaItemStorage)
        {
            _mediaItemStorage = mediaItemStorage;
            _hostingEnvironment = hostingEnvironment;
            _bucketService = bucketService;
            _settingService = settingService;
            _bucketImageService = bucketImageService;
        }
        /// <summary>
        /// 获取签名字符串
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // oss/upload/signature
        [Route("signature")]
        [HttpPost]
        public IActionResult Signature([FromBody]SignatureModel model)
        {
            if (!ModelState.IsValid)
            {
                ApiData.code = 1005;
                ApiData.msg = ModelState.GetErrMsg();
                return Ok(ApiData);
            }
            var settings = _settingService.GetMasterSettings();
            if (String.IsNullOrEmpty(settings.OSSAccessKeyId) || String.IsNullOrEmpty(settings.OSSAccessKeyId))
            {
                ApiData.code = 2001;
                ApiData.msg = "暂未开放上传操作";
                return Ok(ApiData);
            }
            if (!settings.OSSAccessKeyId.Equals(model.AccessKeyId, StringComparison.InvariantCultureIgnoreCase))
            {
                ApiData.code = 2001;
                ApiData.msg = "AccessKeyId错误";
                return Ok(ApiData);
            }
            var signatureString = EncryptorHelper.HmacSha1(settings.OSSAccessKeySecret, $"{model.VERB}{model.ContentMD5}");
            ApiData.code = 0;
            ApiData.msg = "获取成功";
            ApiData.data = new { Signature = signatureString };
            return Ok(ApiData);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("image")]
        public async Task<IActionResult> UpImage([FromForm]UploadModel model)
        {
            if (!ModelState.IsValid)
            {
                ApiData.code = 1005;
                ApiData.msg = ModelState.GetErrMsg();
                return Ok(ApiData);
            }
            if (Request.Form.Files == null || Request.Form.Files.Count == 0 || !Request.Form.Files[0].IsImage())
            {
                ApiData.code = 1006;
                ApiData.msg = "请上传图片文件";
                return Ok(ApiData);
            }
            var bucket = _bucketService.GetBucketBayName(model.bucket);
            if (bucket == null)
            {
                ApiData.code = 2001;
                ApiData.msg = "bucket错误";
                return Ok(ApiData);
            }

            IFormFile file = Request.Form.Files[0];
            string sha1 = file.GetSHA1();
            var item = _bucketImageService.GetSHA1(sha1);
            if (item != null)
            {
                ApiData.code = 0;
                ApiData.msg = "上传成功";
                ApiData.data = new { url = $"/oss/imagecn{item.visiturl}" };
                return Ok(ApiData);
            }

            if (!ValidSignature(model.signature, file.GetMD5(), model.VERB))
            {
                ApiData.code = 1005;
                ApiData.msg = "签名验证失败";
                return Ok(ApiData);
            }

            ImageInfoModel info = new ImageInfoModel();
            info.Bucket = bucket.name.ToLower();
            info.Dir = CombGuid.NewGuidAsString().ToLower().Replace("-", "");
            info.Date = DateTime.Now.ToString("yyyy-MM-dd");

            string path = System.IO.Path.Combine(MediaItemConfig.RootDir, info.Bucket, info.Date, info.Dir);
            //保存文件并且获取文件的相对存储路径
            var image = file.CreateImagePathFromStream(_mediaItemStorage, path);
            info.Name = image.NewFileName;
            info.ExtName = image.ExtName?.ToLower();
            string json = JsonSerializer.Serialize(info);
            string visiturl = $"/oss/imagecn/{EncryptorHelper.EncodeBase64(json)}{info.ExtName}";
            _bucketImageService.AddImage(new Entities.bucket_image()
            {
                id = CombGuid.NewGuidAsString(),
                bucket_id = bucket.id,
                creation_time = DateTime.Now,
                ext_name = image.ExtName,
                sha1 = sha1,
                visiturl = visiturl,
                io_path = image.IOPath,
                width = image.Width,
                height = image.Height,
                length = file.Length
            });

            ApiData.code = 0;
            ApiData.msg = "上传成功";
            ApiData.data = new { url = visiturl };
            await Task.FromResult(0);
            return Ok(ApiData);
        }



        #region 私有方法

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="contentMD5"></param>
        /// <param name="VERB"></param>
        /// <returns></returns>
        private bool ValidSignature(string signature, string contentMD5, string VERB)
        {
            return true;
            //var settings = _settingService.GetMasterSettings();
            //string signatureString = EncryptorHelper.HmacSha1(settings.OSSAccessKeySecret, $"{VERB}{contentMD5}");
            //return signatureString.Equals(signature, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion




    }
}
