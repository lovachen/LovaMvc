using cts.web.core.Librs;
using Lova.Mvc.Areas.Admin.Models;
using Lova.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Controllers
{
    public class LoginController : AreaBaseController
    {
        private SysUserService _sysUserService;

        public LoginController(SysUserService sysUserService)
        {
            _sysUserService = sysUserService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("", Name = "adminLogin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("")]
        public IActionResult DoLogin(LoginModel model)
        {
            var res = _sysUserService.ValidateUser(model.Account, model.Password, 0);
            AjaxData.Message = res.Message;
            AjaxData.Success = res.Status;
            if (res.Status)
            {
                //_sysUserAuthentication.SignIn(res.Jwt.Jti, res.User, res.Jwt.Expiration);
                
            }
            return Json(AjaxData);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpGet("salt", Name = "getSalt")]
        public IActionResult GetSalt(string account)
        {
            var item = _sysUserService.GetSalt(account);
            AjaxData.Data = new { Salt = item ?? "" };
            AjaxData.Success = true;
            AjaxData.Message = "获取成功";
            return Json(AjaxData);
        }
    }
}
