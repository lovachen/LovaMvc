using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lova.Framework;
using Lova.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary> 
    public class SettingController : AdminPrmController
    {

        private SettingService _settingService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingService"></param>
        public SettingController(SettingService settingService)
        {
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


    }
}