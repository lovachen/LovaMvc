using Lova.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Components
{ 
    /// <summary>
  /// 
  /// </summary>
    public class RolesViewComponent : ViewComponent
    {
        private SysRoleService _sysRoleService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysRoleService"></param>
        public RolesViewComponent(SysRoleService sysRoleService)
        {
            _sysRoleService = sysRoleService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = _sysRoleService.GetRoles();

            await Task.FromResult(0);
            return View(roles);
        }
    }
}
