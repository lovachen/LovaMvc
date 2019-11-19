using cts.web.core.Jwt;
using Lova.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

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













    }
}
