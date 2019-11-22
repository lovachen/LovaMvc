using cts.web.core.Menu;
using Lova.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        private IWebHostEnvironment _webHostEnvironment;
        private SysCategoryService _sysCategoryService;
         
        public CategoryController(IWebHostEnvironment webHostEnvironment,
            SysCategoryService sysCategoryService)
        {
            _webHostEnvironment = webHostEnvironment;
            _sysCategoryService = sysCategoryService;
        }

        /// <summary>
        /// 功能菜单列表
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "categoryIndex")]
        public IActionResult CategoryIndex()
        { 
            return View(GetCategories());
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        [Route("init", Name = "initCategory")]
        public IActionResult InitCategory()
        {
            _sysCategoryService.Init(GetCategories());
            AjaxData.Success = true;
            AjaxData.Message = "初始化成功";
            return Json(AjaxData);
        }

        #region 私有方法

        private List<Entities.sys_category> GetCategories()
        {
            List<Entities.sys_category> list = new List<Entities.sys_category>();
            var xmlSiteMap = new XmlSiteMap();
            xmlSiteMap.LoadFrom(Path.Combine(_webHostEnvironment.ContentRootPath, "sitemap.xml"));
            xmlSiteMap.SiteMapNodes.ForEach(item =>
            {
                list.Add(new Entities.sys_category()
                {
                    id = CombGuid.NewGuidAsString(),
                    name = item.Name,
                    route_template = item.RouteTemplate ?? "",
                    code = item.Code,
                    father_code = item.FatherCode,
                    uid = item.UID,
                    target = item.Target ?? "",
                    is_menu = item.IsMenu == "1" ? true : false,
                    sort = item.Sort,
                    action = item.Action ?? "",
                    controller = item.Controller ?? "",
                    icon_class = item.IconClass ?? "",
                    route_name = item.RouteName ?? ""
                });
            });
            return list;
        }

        #endregion
    }
}
