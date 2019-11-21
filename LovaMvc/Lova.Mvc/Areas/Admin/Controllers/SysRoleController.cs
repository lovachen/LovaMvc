using AutoMapper;
using Lova.Mapping;
using Lova.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class SysRoleController : AdminPrmController
    {
        private SysRoleService _sysRoleService;
        private SysUserService _sysUserService;
        private SysCategoryService _sysCategoryService;
        private IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysRoleService"></param>
        /// <param name="sysUserService"></param>
        /// <param name="sysCategoryService"></param>
        /// <param name="mapper"></param>
        public SysRoleController(SysRoleService sysRoleService,
            SysUserService sysUserService,
            SysCategoryService sysCategoryService,
            IMapper mapper)
        {
            _sysCategoryService = sysCategoryService;
            _sysUserService = sysUserService;
            _mapper = mapper;
            _sysRoleService = sysRoleService;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "roleIndex")]
        public IActionResult RoleIndex()
        {
            return View(_sysRoleService.GetRoles());
        }

        /// <summary>
        /// 查看角色详情
        /// </summary>
        /// <returns></returns>
        [Route("users", Name = "roleUsers")]
        public IActionResult RoleUsers(string id)
        {
            var role = _sysRoleService.GetRoleMapping(id);
            role.SysUsers = _sysUserService.GetRoleUsers(id);
            return PartialView(role);
        }

        /// <summary>
        /// 移除角色关联的用户
        /// </summary>
        /// <returns></returns>
        [Route("del/users", Name = "deleteRoleUsers")]
        public IActionResult DeleteRoleUsers(string id, string roleId)
        {
            _sysRoleService.DeleteUserRole(id, roleId, UserId);
            AjaxData.Success =  true;
            AjaxData.Message = "删除成功";
            return Json(AjaxData);
        }


        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <returns></returns>
        [Route("edit", Name = "roleEdit")]
        public IActionResult RoleEdit(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var role = _sysRoleService.GetRoleMapping(id);
                return PartialView(role);
            }
            return PartialView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public IActionResult RoleEdit(Sys_RoleMapping Role)
        { 
            (bool Status, string Message) res;
            if (!String.IsNullOrEmpty(Role.id))
            {
                res = _sysRoleService.UpdateRole(Role, UserId);
            }
            else
            {
                var item = _mapper.Map<Entities.sys_role>(Role);
                item.id = CombGuid.NewGuidAsString();
                item.creation_time = DateTime.Now;
                item.creator = UserId;
                res = _sysRoleService.AddRole(item);
            }
            AjaxData.Message = res.Message;
            AjaxData.Success = res.Status;
            return Json(AjaxData);
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <returns></returns>
        [Route("prm", Name = "rolePrm")]
        public IActionResult RolePrm(string id)
        {
            var role = _sysRoleService.GetRoleMapping(id);
            var categories = _sysCategoryService.GetAllCache();
            ViewBag.Categories = categories.Where(o => o.target == "0").ToList();
            role.SysPermissions = _sysRoleService.GetRolePermissons().Where(o => o.role_id == id).ToList();
            return PartialView(role);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        [HttpPost("prm")]
        public IActionResult RolePrm(string id, List<string> categoryIds)
        {
            var res = _sysRoleService.SetRolePermission(id, categoryIds, UserId);
            AjaxData.Message = res.Message;
            AjaxData.Success = res.Status;
            return Json(AjaxData);
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        [Route("delete", Name = "roleDelete")]
        public IActionResult Delete(string id)
        {
            var res = _sysRoleService.DeleteRole(id, UserId);
            AjaxData.Message = res.Message;
            AjaxData.Success = res.Status;
            return Json(AjaxData);
        }

    }
}
