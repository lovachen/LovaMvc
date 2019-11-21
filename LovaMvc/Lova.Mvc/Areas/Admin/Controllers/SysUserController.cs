using AutoMapper;
using cts.web.core.Datatable;
using cts.web.core.Librs;
using Lova.Framework.Filters;
using Lova.Mapping;
using Lova.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class SysUserController : AdminPrmController
    {
        private SysUserService _sysUserService;
        private SysRoleService _sysRoleService;
        private SysUserLoginService _sysUserLoginLogService;
        private ActivityLogService _activityLogService;
        private SysUserJwtService _sysUserJwtService;
        private IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        public SysUserController(SysUserService sysUserService,
            SysRoleService sysRoleService,
            SysUserLoginService sysUserLoginLogService,
            ActivityLogService activityLogService,
            SysUserJwtService sysUserJwtService,
            IMapper mapper)
        {
            _sysUserJwtService = sysUserJwtService;
            _sysUserLoginLogService = sysUserLoginLogService;
            _sysRoleService = sysRoleService;
            _sysUserService = sysUserService;
            _activityLogService = activityLogService;
            _mapper = mapper;
        }


        [Route("", Name = "userIndex")]
        public IActionResult UserIndex()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("data", Name = "userData"), PrmSpare("userIndex")]
        public IActionResult UserData(SysUserSearchArg arg)
        {
            var parms = Request.QueryString.ToTableParms();
            var pageList = _sysUserService.AdminSearch(arg, parms);

            if (pageList.Any())
            {
                foreach (var user in pageList)
                {
                    user.SysRoles = _sysRoleService.GetUserRoles(user.id);
                }
            }
            var data = pageList.ToAjax();
            return Json(data);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("details", Name = "userDetails")]
        public IActionResult UserDetails(string id)
        {
            var user = _sysUserService.GetUserMapping(id);
            user.SysRoles = _sysRoleService.GetUserRoles(id);
            return PartialView(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="draw"></param>
        /// <returns></returns>
        [Route("dtl/loginlogs", Name = "userLoginLogs"), PrmSpare("userDetails")]
        public IActionResult UserLoginLogs(string id, int draw)
        {
            var list = _sysUserLoginLogService.GetLastUserLogins(id);
            DatatableModel<Sys_UserLoginMapping> data = new DatatableModel<Sys_UserLoginMapping>(list);
            data.draw = draw;
            return Json(data);
        }

        /// <summary>
        /// 获取操作的最新记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="draw"></param>
        [Route("dtl/activitylogs", Name = "userActivityLogs"), PrmSpare("userDetails")]
        public IActionResult ActivityLog(string id, int draw)
        {
            var list = _activityLogService.GetLastUserActivityLogs(id);
            DatatableModel<Sys_ActivityLogMapping> data = new DatatableModel<Sys_ActivityLogMapping>(list);
            data.draw = draw;
            return Json(data);
        }

        /// <summary>
        /// 获取登陆的JwtToken
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="id"></param>
        [Route("dtl/userjwt", Name = "userJwtToken"), PrmSpare("userDetails")]
        public IActionResult UserJwtToken(string id, int draw)
        {
            var list = _sysUserJwtService.GetLastUserJwt(id);
            DatatableModel<Sys_UserJwtMapping> data = new DatatableModel<Sys_UserJwtMapping>(list);
            data.draw = draw;
            return Json(data);
        }

        /// <summary>
        /// 添加人员
        /// </summary>
        /// <returns></returns>
        [Route("edit", Name = "userEdit")]
        public IActionResult UserEdit(string id)
        {
            Sys_UserMapping model = null;
            ViewBag.Roles = _sysRoleService.GetRoles();
            if (!String.IsNullOrEmpty(id))
            {
                model = _sysUserService.GetUserMapping(id);
                if (model != null)
                {
                    var userRoles = _sysRoleService.GetUserRoles();
                    model.UserRoles = userRoles.Where(o => o.user_id == id).Distinct().ToList();
                }
            }
            if (model == null)
                model = new Sys_UserMapping();
            return PartialView(model);
        }

        [HttpPost("edit")]
        public IActionResult UserEdit(Sys_UserMapping SysUser, List<string> RoleIds)
        {
            (bool Status, string Message) res;
            var item = _mapper.Map<Entities.sys_user>(SysUser);

            if (!String.IsNullOrEmpty(SysUser.id))
            {
                res = _sysUserService.UpdateUser(SysUser, UserId);
            }
            else
            {
                item.account = item.account.TrimSpace();
                item.id = CombGuid.NewGuidAsString();
                item.creation_time = DateTime.Now;
                item.creator = UserId;
                item.salt = EncryptorHelper.CreateSaltKey();
                item.password = (EncryptorHelper.GetMD5(item.account + item.salt));
                res = _sysUserService.AddUser(item);
            }
            AjaxData.Message = res.Message;
            AjaxData.Success = res.Status;
            if (res.Status)
            {
                _sysRoleService.SetUserRoles(item.id, RoleIds, UserId);
            }
            return Json(AjaxData);
        }
         

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("roles", Name = "userRoles")]
        public IActionResult UserRoles(string id)
        {
            var user = _sysUserService.GetUserMapping(id);
            user.SysRoles = _sysRoleService.GetUserRoles(id);
            ViewBag.Roles = _sysRoleService.GetRoles();

            return PartialView(user);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="RoleIds"></param>
        /// <returns></returns>
        [HttpPost("roles")]
        public IActionResult UserRoles(string id, List<string> RoleIds)
        {
            _sysRoleService.SetUserRoles(id, RoleIds, UserId);
            AjaxData.Success = true;
            AjaxData.Message = "保存成功";
            return Json(AjaxData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("delete", Name = "userDelete")]
        public IActionResult Delete(string id)
        {
            _sysUserService.Delete(id, UserId);
            try
            {
                _sysUserJwtService.CompelOut(id);
                _sysRoleService.SetUserRoles(id, null, UserId);
            }
            catch (Exception)
            {

            }
            AjaxData.Success = true;
            AjaxData.Message = "删除成功";
            return Json(AjaxData);
        }


    }
}
