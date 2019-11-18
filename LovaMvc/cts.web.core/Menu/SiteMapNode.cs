using cts.web.core.Librs;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core.Menu
{
    /// <summary>
    /// 
    /// </summary>
    public class SiteMapNode
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 唯一标识，一旦确认，将无法在修改。否则会导致WPF无法识别
        /// 明明规则大写，AREA.CONTROLLER.ACTION
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            get
            {
                return EncryptorHelper.GetMD5(UID ?? "");
            }
        }

        /// <summary>
        /// 菜单 1：是,0:否
        /// </summary>
        public string IsMenu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FatherCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RouteTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 自动读取循序设置
        /// </summary>
        public int Sort { get; set; }
         
        /// <summary>
        /// 
        /// </summary>
        public string IconClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 分类标识
        /// </summary>
        public string Target { get; set; }



    }
}
