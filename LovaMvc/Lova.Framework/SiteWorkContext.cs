using Lova.Mapping;
using Lova.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lova.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class SiteWorkContext
    {
        private SettingService _settingService;


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
