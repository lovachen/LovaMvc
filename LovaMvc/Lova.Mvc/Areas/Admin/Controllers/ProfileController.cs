using Lova.Framework.Security;
using Lova.Mapping;
using Lova.Mvc.Areas.Admin.Models;
using Lova.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class ProfileController : AdminController
    {
        private SysUserService _sysUserService;
        private SysUserLoginService _sysUserLoginService;
        private SysUserAuthentication _sysUserAuthentication;

        public ProfileController(SysUserService sysUserService,
            SysUserLoginService sysUserLoginService,
            SysUserAuthentication sysUserAuthentication)
        {
            _sysUserService = sysUserService;
            _sysUserLoginService = sysUserLoginService;
            _sysUserAuthentication = sysUserAuthentication;
        }


        /// <summary>
        /// 个人首页
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "profileIndex")]
        public IActionResult ProfileIndex()
        {
            ProfileModel model = new ProfileModel();
            model.User = _sysUserService.GetUserMapping(UserId);
            model.ChangePassword.Salt = model.User.salt;
            model.ChangePassword.OldPassword = model.User.password;
            model.LoginLogList = _sysUserLoginService.GetLastUserLogins(UserId);
            return View(model);
        }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <returns></returns>
        [HttpPost("changeInfo", Name = "changeInfo")]
        public IActionResult ChangeInfo(Sys_UserMapping model)
        {
            model.name = model.name.Trim();
            model.id = UserId;
            var res = _sysUserService.UpdateUser(model, UserId);
            return Json(res);
        }

        /// <summary>
        /// 个人修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("changePwd", Name = "changPassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            model.Password = model.Password.Trim();
            var res = _sysUserService.UpdatePwd(UserId, model.OldPassword, model.Password, UserId);
            return Json(res);
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        [Route("out", Name = "adminSignOut")]
        public IActionResult AdminSignOut()
        {
            _sysUserAuthentication.SignOut(0);
            return RedirectToRoute("adminLogin");
        }











    }
}
