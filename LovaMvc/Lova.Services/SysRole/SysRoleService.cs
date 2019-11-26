using AutoMapper;
using cts.web.core;
using cts.web.core.Cache;
using Lova.Entities;
using Lova.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Lova.Services
{
    public class SysRoleService:BaseService
    {
        private readonly static object lockObj = new object();
        private const string MODEL_ALL = "lova.sys.roles.all";
        private const string PERMISSION_ALL = "lova.sys.role.permission.all";
        private const string USER_ROLES_ALL = "lova.sys.role.userroles.all";

        private LovaDbContext _dbContext;
        private ICacheManager _cacheManager;
        private IMapper _mapper;
        private ActivityLogService _activityLogService;
        ILogger<SysRoleService> _logger;

        public SysRoleService(ICacheManager cacheManager,
            LovaDbContext dbContext,
            IMapper mapper,
            ILogger<SysRoleService> logger,
            ActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
            _mapper = mapper;
            _dbContext = dbContext;
            _cacheManager = cacheManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取所有的roles 并缓存
        /// </summary>
        /// <returns></returns>
        public List<Sys_RoleMapping> GetRoles()
        {
            return _cacheManager.Get<List<Sys_RoleMapping>>(MODEL_ALL, 60 * 24, () =>
            {
                return _dbContext.sys_role.Select(item => new Sys_RoleMapping()
                {
                    id = item.id,
                    creation_time = item.creation_time,
                    name = item.name,
                    description = item.description,
                }).ToList();
            });
        }

        /// <summary>
        /// 获取所有的roles 并缓存
        /// </summary>
        /// <returns></returns>
        public List<Sys_PermissionMapping> GetRolePermissons()
        {
            return _cacheManager.Get<List<Sys_PermissionMapping>>(PERMISSION_ALL, () =>
            {
                return _dbContext.sys_permission
                .Select(item => new Sys_PermissionMapping()
                {
                    id = item.id,
                    role_id= item.role_id,
                    category_id = item.category_id
                }).ToList();
            });
        }

        /// <summary>
        /// 获取所有的用户角色 并缓存
        /// </summary>
        /// <returns></returns>
        public List<Sys_UserRoleMapping> GetUserRoles()
        {
            return _cacheManager.Get<List<Sys_UserRoleMapping>>(USER_ROLES_ALL, () =>
            {
                return _dbContext.sys_user_role.Select(item => new
                Sys_UserRoleMapping()
                {
                    role_id = item.role_id,
                    user_id = item.user_id,
                    id = item.id
                }).ToList();
            });
        }

        /// <summary>
        /// 获取用户的角色信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Sys_RoleMapping> GetUserRoles(string userId)
        {
            var roles = GetRoles();
            var userRoles = GetUserRoles();
            if (roles != null && userRoles != null)
                return userRoles.Where(o => o.user_id == userId).Join(roles, ur => ur.role_id, r => r.id, (ur, r) => r).Distinct().ToList();
            return null;
        }

        /// <summary>
        /// 配置用户角色
        /// </summary>
        /// <param name="userRoles"></param>
        public void SetUserRoles(string userId, List<string> roleIds, string modifier)
        {
            try
            {
                using (var trans = _dbContext.Database.BeginTransaction())
                {
                    _dbContext.Database.ExecuteSqlRaw($"DELETE FROM sys_user_role WHERE id!='' AND user_id='{userId}'");
                    if (roleIds != null && roleIds.Any())
                        roleIds.ForEach(roleId =>
                        {
                            _dbContext.sys_user_role.Add(new sys_user_role()
                            {
                                id = CombGuid.NewGuidAsString(),
                                role_id = roleId,
                                user_id = userId
                            });
                        });
                    _dbContext.SaveChanges();
                    trans.Commit();
                    _cacheManager.Remove(USER_ROLES_ALL);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }


        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <param name="modifier"></param>
        public void DeleteUserRole(string userId, string roleId, string modifier)
        {
            var user_role = _dbContext.sys_user_role.FirstOrDefault(o => o.role_id == roleId && o.user_id == userId);
            if (user_role == null) return;

            string oldLog = JsonSerializer.Serialize(user_role);

            _dbContext.sys_user_role.Remove(user_role);
            _dbContext.SaveChanges();

            _activityLogService.DeletedEntity<Entities.sys_user_role>(user_role, oldLog, null, modifier);
        }

        /// <summary>
        /// 从数据库获取角色详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_RoleMapping GetRoleMapping(string id)
        {
            var item = _dbContext.sys_role.AsNoTracking().FirstOrDefault(o => o.id == id);
            return item == null ? null : _mapper.Map<Sys_RoleMapping>(item);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public (bool Status, string Message) AddRole(Entities.sys_role role)
        {
            lock (lockObj)
            {
                if (_dbContext.sys_role.Any(o => o.name == role.name))
                {
                    return Fail("角色名称已经存在");
                }
                _dbContext.sys_role.Add(role);
                _dbContext.SaveChanges();

                string newJson = JsonSerializer.Serialize(role);
                _activityLogService.InsertedEntity<Entities.sys_role>(role.id, null, newJson, role.creator);
                RemoveCahce();
                return Success("添加成功");
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public (bool Status, string Message) UpdateRole(Sys_RoleMapping role, string modifier)
        {
            lock (lockObj)
            {
                var item = _dbContext.sys_role.Find(role.id);
                if (item == null) return Fail("角色不存在");
                string oldLog = JsonSerializer.Serialize(item);
                item.name = role.name;
                item.description = role.description;
                _dbContext.SaveChanges();
                string newLog = JsonSerializer.Serialize(item);
                _activityLogService.UpdatedEntity<Entities.sys_role>(item.id, oldLog, newLog, modifier);
                RemoveCahce();
                return Success("修改成功");
            }
        }

        /// <summary>
        /// 删除角色，将删除角色所有的权限配置
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public (bool Status, string Message) DeleteRole(string roleId, string modifier)
        {
            lock (lockObj)
            {
                using (var trans = _dbContext.Database.BeginTransaction())
                {
                    var item = _dbContext.sys_role.Find(roleId);
                    if (item == null) return Fail("角色不存在");
                    string oldLog = JsonSerializer.Serialize(item);

                    _dbContext.Database.ExecuteSqlRaw($"DELETE FROM sys_permission WHERE id!='' AND role_id='{item.id}'");
                    _dbContext.Database.ExecuteSqlRaw($"DELETE FROM sys_user_role WHERE id!='' AND role_id='{item.id}'");

                    _dbContext.sys_role.Remove(item);
                    _dbContext.SaveChanges();
                    _activityLogService.UpdatedEntity<Entities.sys_role>(item.id, oldLog, null, modifier);
                    trans.Commit();

                    //
                    RemoveCahce();
                    Remove_PRM_USER();
                    return Success("删除成功");
                }
            }
        }

        /// <summary>
        /// 获取用户的所有权限数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Sys_PermissionMapping> GetUserPermissions(string userId)
        {
            var userRoles = GetUserRoles();
            var rolePermissions = GetRolePermissons();
            if (userRoles != null && rolePermissions != null)
            {
                return userRoles.Where(o => o.user_id == userId)
                      .Join(rolePermissions, ur => ur.role_id, rp => rp.role_id, (a, b) => b).Distinct().ToList();
            }
            return null;
        }


        /// <summary>
        /// 配置，角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="categoryIds"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public (bool Status, string Message) SetRolePermission(string roleId, List<string> categoryIds, string modifier)
        {
            lock (lockObj)
            {
                if (!_dbContext.sys_role.Any(o => o.id == roleId))
                {
                    return Fail("角色不存在或已被删除");
                }
                using (var trans = _dbContext.Database.BeginTransaction())
                {
                    _dbContext.Database.ExecuteSqlRaw($"DELETE FROM sys_permission WHERE id!='' AND role_id='{roleId}'");
                    categoryIds.ForEach(id =>
                    {
                        _dbContext.sys_permission.Add(new sys_permission()
                        {
                            id = CombGuid.NewGuidAsString(),
                            category_id = id,
                            role_id = roleId
                        });
                    });
                    _dbContext.SaveChanges();
                    trans.Commit();
                }
            }
            _cacheManager.Remove(PERMISSION_ALL);
            return Success("保存成功");
        }

        #region 私有方法


        private void RemoveCahce()
        {
            _cacheManager.Remove(MODEL_ALL);
        }

        private void Remove_PRM_USER()
        {
            _cacheManager.Remove(PERMISSION_ALL);
            _cacheManager.Remove(USER_ROLES_ALL);
        }

        #endregion

    }
}
