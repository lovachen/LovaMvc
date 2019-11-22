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

        public BucketCutController(BucketCutService bucketCutService)
        {
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
        public IActionResult EditBucketCut(Entities.bucket_cut model)
        {
            if (!ModelState.IsValid)
            {
                AjaxData.Message = ModelState.GetErrMsg();
                return Json(AjaxData);
            }
            model.id = CombGuid.NewGuidAsString();
            model.creation_time = DateTime.Now;
            model.creator = UserId;
            _bucketCutService.InsertCut(model);
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
        public IActionResult Delete(Guid id)
        {
            _bucketCutService.DeleteCut(id);
            AjaxData.Success = true;
            AjaxData.Message = "删除成功";
            return Json(AjaxData);
        }









    }
}
