using Lova.Framework.Model;
using Lova.Framework.Security;
using Lova.Mapping;
using Lova.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lova.Framework
{
    public class WorkContext
    {
        private readonly static string ITEMS_CATEGORYIES_KEY = "ab.items.categories";

        private SysUserAuthentication _sysUserAuthentication;
        private SysRoleService _sysRoleService;
        private SysCategoryService _sysCategoryService;
        private IHttpContextAccessor _httpContextAccessor;
        private SysUserService _sysUserService;

        public WorkContext(SysUserAuthentication sysUserAuthentication,
            SysRoleService sysRoleService,
            IHttpContextAccessor httpContextAccessor,
            SysCategoryService sysCategoryService,
            SysUserService sysUserService)
        {
            _sysRoleService = sysRoleService;
            _sysCategoryService = sysCategoryService;
            _sysUserAuthentication = sysUserAuthentication;
            _httpContextAccessor = httpContextAccessor;
            _sysUserService = sysUserService;
        }






        /// <summary>
        /// 获取保存在cookie或者jwt的简单用户信息
        /// </summary>
        public UserData GetUserData(int platform)
        {
            return _sysUserAuthentication.GetData(platform);
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="platform"></param>
        public void SignOut(int platform = 0)
        {
            _sysUserAuthentication.SignOut(platform);
        }

        /// <summary>
        /// 获取登陆用户的ID
        /// </summary>
        /// <returns></returns>
        public string GetUserId(int platform = 0)
        {
            return _sysUserAuthentication.GetUserId(platform);
        }

        /// <summary>
        /// 获取我的权限
        /// </summary>
        /// <param name="plaftorm"></param>
        /// <returns></returns>
        public List<Sys_CategoryMapping> GetMyCategories(int plaftorm = 0)
        {
            var data = _httpContextAccessor.HttpContext.Items[ITEMS_CATEGORYIES_KEY] as List<Sys_CategoryMapping>;
            if (data == null)
            {
                var user = GetUserData(plaftorm);
                var categories = _sysCategoryService.GetAllCache().Where(o => o.target == plaftorm.ToString()).ToList();
                if (user.IsAdmin)
                {
                    return categories;
                }
                var userPermissions = _sysRoleService.GetUserPermissions(user.Id);
                if (userPermissions != null)
                {
                    data = userPermissions.Join(categories, up => up.category_id, c => c.id, (a, b) => b).Distinct().ToList();
                    if (data != null)
                        _httpContextAccessor.HttpContext.Items[ITEMS_CATEGORYIES_KEY] = data;
                }
            }
            return data;
        }

        /// <summary>
        /// 获取当前已经登陆的信息
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public Sys_UserMapping GetUser(int platform = 0)
        {
            var userId = GetUserId(platform);
            return _sysUserService.GetLogged(userId);
        }

        /// <summary>
        /// 网页端检测是否含有权限
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool AuthorityCheck(string routeNameOrPath)
        {
            var categories = GetMyCategories(0);
            return categories != null && categories.Any(o => o.route_name.Equals(routeNameOrPath, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// 权限检测
        /// </summary>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public bool AuthorityCheck(string action, string controller)
        {
            var categories = GetMyCategories(0);
            return categories != null && categories.Any(o => o.action.Equals(action, StringComparison.InvariantCultureIgnoreCase)
            && o.controller.Equals(controller, StringComparison.InvariantCultureIgnoreCase));
        }
         






    }
}
