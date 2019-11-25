using cts.web.core.Datatable;
using Lova.Framework.Filters;
using Lova.Mapping;
using Lova.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class BucketImagesController : AdminPrmController
    {
        private BucketImageService _bucketImagesService;
        private BucketService _bucketService;

        public BucketImagesController(BucketImageService bucketImagesService,
            BucketService bucketService)
        {
            _bucketService = bucketService;
            _bucketImagesService = bucketImagesService;
        }




        /// <summary>
        /// 图片列表预览
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "bucketImagesIndex")]
        public ActionResult BucketImagesIndex()
        {
           var buckets = _bucketService.AllBuckets();
            return View(buckets);
        }


        [Route("data",Name = "imageData"), PrmSpare("bucketImagesIndex")]
        public ActionResult ImageData(BucketImageSearchArg arg)
        {
            var parms = Request.QueryString.ToTableParms();
            var pageList = _bucketImagesService.AdminSearch(arg, parms);
            return Json(pageList.ToAjax());
        }















    }
}
