using AutoMapper;
using cts.web.core;
using cts.web.core.Cache;
using cts.web.core.Librs;
using Lova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lova.Services
{
    public class SysUserService
    {
        private readonly static object addLock = new object();
        private readonly static string BY_ID = "lova.services.sys_user.byid-{0}";

        private ICacheManager _cacheManager;
        private LovaDbContext _dbContext;
        private IWebHelper _webHelper;
        private IMapper _mapper;
        //ActivityLogService _activityLogService;

        public SysUserService(ICacheManager cacheManager,
            LovaDbContext dbContext,
            IWebHelper webHelper,
            //ActivityLogService activityLogService,
            IMapper mapper)
        {
            //_activityLogService = activityLogService;
            _mapper = mapper;
            _webHelper = webHelper;
            _dbContext = dbContext;
            _cacheManager = cacheManager;
        }


        /// <summary>
        /// 验证登陆时获取
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GetSalt(string account)
        {
            return _dbContext.sys_user.Where(o => o.account == account && !o.is_deleted)
                 .Select(item => item.salt).FirstOrDefault();
        }

        /// <summary>
        /// 用户登陆验证
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="platform">0：web，1：app</param>
        /// <returns></returns>
        public (bool Status, string Message, Entities.sys_user User, Entities.sys_user_jwt Jwt) ValidateUser(string account, string password, int platform = 0)
        {
            var user = _dbContext.sys_user.Where(o => o.account == account && !o.is_deleted).FirstOrDefault();
            if (user == null) return (false, "账号或密码错误", null, null);
            bool success = false;
            var log = new Entities.sys_user_login()
            {
                id = CombGuid.NewGuidAsString(),
                user_id = user.id,
                ip_addr = _webHelper.GetIPAddress(),
                logged_time = DateTime.Now,
            };
            Entities.sys_user_jwt jwt = null;
            string msg = "账号或密码错误";
            if (!String.IsNullOrEmpty(user.password) && password.Equals(user.password, StringComparison.InvariantCultureIgnoreCase))
            {
                success = true;
                msg = "登陆成功";
                user.last_ipaddr = log.ip_addr;
                jwt = new Entities.sys_user_jwt()
                {
                    id = CombGuid.NewGuidAsString(), 
                    expiration = DateTime.Now.AddDays(30),
                    platform = platform,
                    user_id = user.id
                };
                _dbContext.sys_user_jwt.Add(jwt);
            }
            _dbContext.sys_user_login.Add(log);
            _dbContext.SaveChanges();
            return (success, msg, user, jwt);
        }

    }
}
