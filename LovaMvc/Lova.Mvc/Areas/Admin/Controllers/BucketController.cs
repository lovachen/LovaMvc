using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lova.Entities;
using Lova.Mapping;
using Lova.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class BucketController : AdminPrmController
    { 
        private BucketService _bucketService; 
        private BucketCutService _bucketCutService;
        private IMapper _mapper;

        public BucketController(BucketService bucketService,
            BucketCutService bucketCutService,
            IMapper mapper)
        {
            _mapper = mapper;
            _bucketCutService = bucketCutService;
            _bucketService = bucketService; 
        }

        /// <summary>
        /// bucket列表
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "bucketIndex")]
        public IActionResult BucketIndex()
        {
            var buckets = _bucketService.AllBuckets();
            var cutList = _bucketCutService.AllBucketCuts();
            if (buckets != null)
            {
                buckets.ForEach(item =>
                {
                    item.BucketCuts = cutList.Where(o => o.bucket_id == item.id).ToList();
                });
            }
            return View(buckets);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("edit", Name = "editBucket")]
        public IActionResult EditBucket(string id)
        {
            BucketMapping model = new BucketMapping();
            if (!String.IsNullOrEmpty(id))
            {
                model = _bucketService.GetBucketMapping(id);
            }
            return PartialView(model);
        }


        [HttpPost]
        [Route("edit")]
        public IActionResult EditBucket(BucketMapping model)
        { 
            if (!String.IsNullOrEmpty(model.id))
            {
                _bucketService.UpdateBucket(model, UserId);
                AjaxData.Message = "修改成功";
            }
            else
            {
                var entity = _mapper.Map<Entities.bucket>(model);
                entity.id = CombGuid.NewGuidAsString();
                entity.creator = UserId;
                entity.creation_time = DateTime.Now;
                _bucketService.AddBucket(entity);
                AjaxData.Message = "添加成功";
            }
            AjaxData.Success = true;
            return Json(AjaxData);
        }



    }
}
