using cts.web.core.Jwt;
using Lova.Framework.Model;
using Lova.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Lova.Framework.Security
{
    public class SysUserAuthentication
    {
        private readonly static string USER_DATA_ITEMS_KEY = "lova.items.sysuser";
        private IHttpContextAccessor _httpContextAccessor;
        private SysUserJwtService _sysUserJwtService;
        private IJWTFactory _jwtFactory;

        public SysUserAuthentication(IHttpContextAccessor httpContextAccessor,
            SysUserJwtService sysUserJwtService,
            IJWTFactory jwtFactory)
        {
            _jwtFactory = jwtFactory;
            _sysUserJwtService = sysUserJwtService;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// 保存登陆状态
        /// 为了和jwt保存同步 同用JwtRegisteredClaimNames.Jti
        /// </summary>
        /// <param name="jwtid"></param>
        /// <param name="user"></param>
        /// <param name="expires"></param>
        /// <param name="platform">0：web后台，1：app</param>
        /// <returns>当jwt标识登陆时返回string</returns>
        public string SignIn(string jwtid, Entities.sys_user user, DateTime expires, int platform = 0)
        {
            string userDataJson = JsonSerializer.Serialize(new UserData() { Id = user.id, Name = user.name, Account = user.account, IsAdmin = user.is_admin });

            switch (platform)
            {
                case 0:
                    List<Claim> claims = new List<Claim>() {
                        new Claim(JwtRegisteredClaimNames.Jti, jwtid),
                        new Claim(ClaimTypes.Sid,user.id),
                        new Claim(ClaimTypes.UserData, userDataJson) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties()
                    {
                        ExpiresUtc = expires
                    });
                    break;
                case 1:
                    return _jwtFactory.CreateToken(new User() { PrimarySid = platform, UserID = user.id, UserData = userDataJson, UserName = user.name }, jwtid, expires);
            }
            return null;
        }










    }
}
