using AutoMapper;
using Lova.Entities;
using Lova.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lova.Services
{
    public class SysUserLoginService
    {
        private LovaDbContext _dbContext;
        private IMapper _mapper;
        private ActivityLogService _activityLogService;

        public SysUserLoginService(LovaDbContext dbContext,
            IMapper mapper,
            ActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// 获取最新的20条登陆记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Sys_UserLoginMapping> GetLastUserLogins(string userId)
        {
            return _dbContext.sys_user_login.Where(o => o.user_id == userId).OrderByDescending(o => o.logged_time).Take(20).ToList()
                .Select(item => _mapper.Map<Sys_UserLoginMapping>(item)).ToList();
        }




    }
}
