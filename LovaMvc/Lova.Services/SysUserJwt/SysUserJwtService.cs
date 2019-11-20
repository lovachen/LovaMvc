using AutoMapper;
using cts.web.core.Cache;
using Lova.Entities;
using Lova.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lova.Services
{
    public class SysUserJwtService
    {
        private readonly static string KEY_BY_JTI = "lova.services.jwt-{0}";

        private ICacheManager _cacheManager;
        private LovaDbContext _dbContext;
        private IMapper _mapper;

        public SysUserJwtService(ICacheManager cacheManager,
            LovaDbContext dbContext,
            IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _cacheManager = cacheManager;
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jtwid"></param>
        /// <returns></returns>
        public Sys_UserJwtMapping GetJwtMapping(string jtwid)
        {
            return _cacheManager.Get<Sys_UserJwtMapping>(String.Format(KEY_BY_JTI, jtwid), () =>
            {
                var item = _dbContext.sys_user_jwt.FirstOrDefault(o => o.id == jtwid);
                return item != null ? _mapper.Map<Sys_UserJwtMapping>(item) : null;
            });
        }



        /// <summary>
        /// 退出登陆，移除jwttoken
        /// </summary>
        /// <param name="jtwid"></param>
        public void SignOut(string jtwid)
        {
            _dbContext.Database.ExecuteSqlRaw($"DELETE FROM [sys_user_jwt] WHERE [id]={jtwid}");
            RemoveCahce(jtwid);
        }

        /// <summary>
        /// 强制用户下线所有平台
        /// </summary>
        /// <param name="userId"></param>
        public void CompelOut(string userId)
        {
            var jwtList = _dbContext.sys_user_jwt.Where(o => o.user_id == userId).ToList();
            _dbContext.sys_user_jwt.RemoveRange(jwtList);
            _dbContext.SaveChanges();
            jwtList.ForEach(item =>
            {
                RemoveCahce(item.id);
            });
        }

        /// <summary>
        /// 获取用户的所有登陆Jwt记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Sys_UserJwtMapping> GetLastUserJwt(string userId)
        {
            return _dbContext.sys_user_jwt.Where(o => o.user_id == userId).OrderByDescending(o => o.expiration)
                .ToList().Select(item => _mapper.Map<Sys_UserJwtMapping>(item)).ToList();
        }


        #region 私有方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jtwid"></param>
        private void RemoveCahce(string jtwid)
        {
            _cacheManager.Remove(String.Format(KEY_BY_JTI, jtwid));

        }


        #endregion











    }
}
