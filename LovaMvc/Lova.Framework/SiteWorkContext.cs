using System;
using System.Collections.Generic;
using System.Text;
using Lova.Mapping;
using Lova.Services;

namespace Lova.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class SiteWorkContext
    {
        SettingService _settingService;


        public SiteWorkContext(SettingService settingService)
        {
            _settingService = settingService;
            this.Settings = _settingService.GetMasterSettings();
        }

        /// <summary>
        /// 配置
        /// </summary>
        public SiteSettings Settings;
    }
}
