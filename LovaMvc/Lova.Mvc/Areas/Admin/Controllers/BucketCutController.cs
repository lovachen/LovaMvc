using AutoMapper;
using Lova.Mapping;
using Lova.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class BucketCutController : AdminPrmController
    {
        private BucketCutService _bucketCutService;
        private IMapper _mapper;

        public BucketCutController(BucketCutService bucketCutService,
            IMapper mapper)
        {
            _mapper = mapper;
            _bucketCutService = bucketCutService;
        }

        /// <summary>
        /// 编辑Bucket缩略尺寸
        /// </summary>
        /// <returns></returns>
        [Route("edit", Name = "editBucketCut")]
        public IActionResult EditBucketCut(string id, string bucketId)
        {
            BucketCutMapping model = new BucketCutMapping() { bucket_id = bucketId };
            if (!String.IsNullOrEmpty(id))
            {
                model = _bucketCutService.GetById(id);
                if (model == null)
                    return Content("数据不存在");
            }
            return PartialView(model);
        }

        [Route("edit")]
        [HttpPost]
        public IActionResult EditBucketCut(BucketCutMapping model)
        {
            if (!ModelState.IsValid)
            {
                AjaxData.Message = ModelState.GetErrMsg();
                return Json(AjaxData);
            }
            var entity = _mapper.Map<Entities.bucket_cut>(model);

            entity.id = CombGuid.NewGuidAsString();
            entity.creation_time = DateTime.Now;
            entity.creator = UserId;
            _bucketCutService.InsertCut(entity);
            AjaxData.Success = true;
            AjaxData.Message = "保存成功";
            return Json(AjaxData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete", Name = "deleteBucketCute")]
        public IActionResult Delete(string id)
        {
            _bucketCutService.DeleteCut(id);
            AjaxData.Success = true;
            AjaxData.Message = "删除成功";
            return Json(AjaxData);
        }









    }
}
