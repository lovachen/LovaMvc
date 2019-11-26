using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lova.Framework;
using Lova.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary> 
    public class SettingController : AdminPrmController
    {
        private MarkLogoService _markLogoService ;
        private SettingService _settingService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="markLogoService"></param>
        /// <param name="settingService"></param>
        public SettingController(SettingService settingService,
            MarkLogoService markLogoService)
        {
            _markLogoService = markLogoService;
            _settingService = settingService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("",Name ="settingIndex")]
        public IActionResult SettingIndex()
        {
            return View(_settingService.GetMasterSettings());
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("edit",Name = "settingEdit")]
        public IActionResult SettingEdit(string pk, string value)
        {
            _settingService.SaveSetting(pk, value);
            AjaxData.Success = true;
            AjaxData.Message = "保存成功";
            return Json(AjaxData);
        }



        /// <summary>
        /// 保存水印图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Route("mark",Name = "saveMark")]
        public IActionResult SaveMark(IFormFile file)
        {
            if (file == null || !file.IsImage())
            {
                AjaxData.Message = "请选择图片文件";
                return Json(AjaxData);
            }
            using (var stream = file.OpenReadStream())
            {
                _markLogoService.SaveStream(stream);
                AjaxData.Message = "保存成功";
                AjaxData.Success=true;
                return Json(AjaxData);
            }
        }

        /// <summary>
        /// 保存图片不存在时显示的底图
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Route("basemap",Name = "saveBaseMap")]
        public IActionResult SaveBaseMap(IFormFile file)
        {
            if (file == null || !file.IsImage())
            {
                AjaxData.Message = "请选择图片文件";
                return Json(AjaxData);
            }
            using (var stream = file.OpenReadStream())
            {
                _markLogoService.Save404Stream(stream);
                AjaxData.Message = "保存成功";
                AjaxData.Success = true;
                return Json(AjaxData);
            }
        }











    }
}