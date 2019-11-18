using cts.web.core.Menu;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lova.Services
{
    public class InstallService
    {
        private IWebHostEnvironment _environment;
        private SysCategoryService _sysCategoryService;
        private SettingService _settingService;

        public InstallService(IWebHostEnvironment environment,
            SysCategoryService sysCategoryService,
            SettingService settingService)
        {
            _sysCategoryService = sysCategoryService;
            _environment = environment;
            _settingService = settingService;
        }


        /// <summary>
        /// 接口初始化
        /// </summary>
        public void Install()
        {
            var xmlSiteMap = new XmlSiteMap();
            xmlSiteMap.LoadFrom(Path.Combine(_environment.ContentRootPath, "sitemap.xml"));
            List<Entities.sys_category> sysApis = new List<Entities.sys_category>();
            xmlSiteMap.SiteMapNodes.ForEach(item =>
            {
                sysApis.Add(new Entities.sys_category()
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
            _sysCategoryService.Init(sysApis);
            _settingService.Init();
        }

    }
}
