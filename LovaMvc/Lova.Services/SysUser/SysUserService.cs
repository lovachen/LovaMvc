using AutoMapper;
using cts.web.core;
using cts.web.core.Cache;
using cts.web.core.Datatable;
using cts.web.core.Librs;
using Lova.Entities;
using Lova.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Lova.Services
{
    public class SysUserService : BaseService
    {
        private readonly static object addLock = new object();
        private readonly static string BY_ID = "lova.services.sys_user.byid-{0}";

        private ICacheManager _cacheManager;
        private LovaDbContext _dbContext;
        private IWebHelper _webHelper;
        private IMapper _mapper;
        private ActivityLogService _activityLogService;

        public SysUserService(ICacheManager cacheManager,
            LovaDbContext dbContext,
            IWebHelper webHelper,
            ActivityLogService activityLogService,
            IMapper mapper)
        {
            _activityLogService = activityLogService;
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

        /// <summary>
        /// 获取已经登陆用户的信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Sys_UserMapping GetLogged(string userId)
        {
            return _cacheManager.Get<Sys_UserMapping>(String.Format(BY_ID, userId), () =>
            {
                var user = _dbContext.sys_user.FirstOrDefault(o => o.id == userId);
                return user != null ? _mapper.Map<Sys_UserMapping>(user) : null;
            });
        }

        /// <summary>
        /// 更改用户资料
        /// </summary>
        /// <param name="user"></param>
        public (bool Status, string Message) UpdateUser(Sys_UserMapping model, string modifier)
        {
            var user = _dbContext.sys_user.Find(model.id);
            if (user == null) Fail("用户不存在");
            string oldJson = JsonSerializer.Serialize(user);

            user.name = model.name;

            string newJson = JsonSerializer.Serialize(user);

            _dbContext.SaveChanges();
            //记录日志
            _activityLogService.InsertedEntity<Entities.sys_user>(model.id, oldJson, newJson, modifier);
            RemoveCache(user.id);
            return Success("修改成功");
        }

        /// <summary>
        /// 删除用户，删除后最好调用强制下线操作
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="modifier"></param>
        public void Delete(string userId, string modifier)
        {

            var user = _dbContext.sys_user.Find(userId);
            if (user == null) return;

            string oldJson = JsonSerializer.Serialize(user);
            user.deleted_time = DateTime.Now;
            user.is_deleted = true;
            _dbContext.SaveChanges();

            string newJson = JsonSerializer.Serialize(user);
            _activityLogService.DeletedEntity<Entities.sys_user>(userId, oldJson, newJson, modifier);
            RemoveCache(user.id);
        }

        /// <summary>
        /// 修改密码，重置密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <param name="modifier"></param>
        /// <param name="reset">重置密码，只有管理员的操作</param>
        /// <returns></returns>
        public (bool Status, string Message) UpdatePwd(string userId, string oldPwd, string newPwd, string modifier, bool reset = false)
        {
            var user = _dbContext.sys_user.Find(userId);
            if (user == null) return (false, "用户不存在");
            string oldJson = JsonSerializer.Serialize(user);

            if (reset)
            {
                user.password = EncryptorHelper.GetMD5(user.account + user.salt);
            }
            else
            {
                if (user.password.Equals(oldPwd, StringComparison.InvariantCultureIgnoreCase))
                {
                    user.password = newPwd;
                }
                else
                {
                    return (false, "原密码错误");
                }
            }
            _dbContext.SaveChanges();
            string newJson = JsonSerializer.Serialize(user);
            _activityLogService.InsertedEntity<Entities.sys_user>(userId, oldJson, newJson, modifier);
            return (true, "修改成功");
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public (bool Status, string Message) AddUser(Entities.sys_user user)
        {
            lock (addLock)
            {
                if (_dbContext.sys_user.Any(o => o.account == user.account && !o.is_deleted))
                {
                    return Fail("用户账号已经存在");
                }
                _dbContext.sys_user.Add(user);
                _dbContext.SaveChanges();
                string newJson = JsonSerializer.Serialize(user);
                _activityLogService.InsertedEntity<Entities.sys_user>(user.id, null, newJson, user.creator);

                return Success("添加成功");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public IPagedList<Sys_UserMapping> AdminSearch(SysUserSearchArg arg, DataTablesParameters parmas)
        {
            var query = _dbContext.sys_user.Where(o => !o.is_deleted);

            if (arg != null)
            {
                if (!String.IsNullOrEmpty(arg.q))
                {
                    arg.q = arg.q.Trim();
                    query = query.Where(o => o.account.Contains(arg.q) || o.name.Contains(arg.q));
                }
            }

            #region 排序

            if (!String.IsNullOrEmpty(parmas.OrderName))
            {
                switch (parmas.OrderName)
                {
                    case "account":
                        if (parmas.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.account);
                        else
                            query = query.OrderBy(o => o.account);
                        break;
                    case "name":
                        if (parmas.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.name);
                        else
                            query = query.OrderBy(o => o.name);
                        break;
                    case "creation_time":
                        if (parmas.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.creation_time);
                        else
                            query = query.OrderBy(o => o.creation_time);
                        break;
                    case "last_activity_time":
                        if (parmas.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.last_activity_time);
                        else
                            query = query.OrderBy(o => o.last_activity_time);
                        break;
                    default:
                        query = query.OrderBy(o => o.id);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.id);
            }
            #endregion

            return PagedList<Sys_UserMapping>.Create<Entities.sys_user>(query, parmas.PageIndex, parmas.Length, _mapper);
        }

        /// <summary>
        /// 获取角色的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<Sys_UserMapping> GetRoleUsers(string roleId)
        {
            var query = from u_r in _dbContext.sys_user_role
                        join u in _dbContext.sys_user on u_r.user_id equals u.id
                        select new Sys_UserMapping()
                        {
                            id = u.id,
                            name = u.name,
                            account = u.account
                        };
            return query.Distinct().ToList();
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_UserMapping GetUserMapping(string id)
        {
            var user = _dbContext.sys_user.AsNoTracking().FirstOrDefault(o => o.id == id);
            return user != null ? _mapper.Map<Sys_UserMapping>(user) : null;
        }

        #region 私有方法

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="userId"></param>
        private void RemoveCache(string userId)
        {
            _cacheManager.Remove(String.Format(BY_ID, userId));
        }

        #endregion

    }
}
