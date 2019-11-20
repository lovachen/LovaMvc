using Lova.Entities;
using Lova.Mapping;
using AutoMapper;
using cts.web.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cts.web.core.Datatable;

namespace Lova.Services
{
    public class ActivityLogService
    {
        private LovaDbContext _dbContext;
        private IMapper _mapper;

        public ActivityLogService(IMapper mapper,
            LovaDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region 操作

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logService"></param>
        /// <param name="entiy"></param>
        public void InsertedEntity<T>(object primaryKey, string oldValue, string newValue, string userId = null)
        {
            InsertActivityLog<T>("新增", primaryKey, oldValue, newValue, userId);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logService"></param>
        /// <param name="entiy"></param>
        public void UpdatedEntity<T>(object primaryKey, string oldValue, string newValue, string userId = null)
        {
            InsertActivityLog<T>("修改", primaryKey, oldValue, newValue, userId);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logService"></param>
        /// <param name="entiy"></param>
        public void DeletedEntity<T>(object primaryKey, string oldValue, string newValue, string userId = null)
        {
            InsertActivityLog<T>("删除", primaryKey, oldValue, newValue, userId);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IPagedList<Sys_ActivityLogMapping> AdminSearch(ActivityLogSearchArg arg, DataTablesParameters parameters)
        {

            var query = from log in _dbContext.sys_activitylog
                        join u in _dbContext.sys_user on log.creator equals u.id
                        join c in _dbContext.sys_activitylog_comment on log.entity_name equals c.entity_name into temp
                        from lcomment in temp.DefaultIfEmpty()
                        select new Sys_ActivityLogMapping()
                        {
                            id = log.id,
                            entity_name = log.entity_name,
                            method = log.method,
                            newvalue = log.newvalue,
                            oldvalue = log.oldvalue,
                            creation_time = log.creation_time,
                            primary_key = log.primary_key,
                            user_name = u.name,
                            user_account = u.account,
                            comment = lcomment.comment
                        };



            #region 排序

            if (!String.IsNullOrEmpty(parameters.OrderName))
            {
                switch (parameters.OrderName)
                {
                    case "Method":
                        if (parameters.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.method);
                        else
                            query = query.OrderBy(o => o.method);
                        break;
                    case "EntityName":
                        if (parameters.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.entity_name);
                        else
                            query = query.OrderBy(o => o.entity_name);
                        break;
                    case "CreationTimeForamt":
                        if (parameters.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.creation_time);
                        else
                            query = query.OrderBy(o => o.creation_time);
                        break;
                    case "UserAccount":
                        if (parameters.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.user_account);
                        else
                            query = query.OrderBy(o => o.user_account);
                        break;
                    case "UserName":
                        if (parameters.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.user_name);
                        else
                            query = query.OrderBy(o => o.user_name);
                        break;
                    default:
                        query = query.OrderBy(o => o.id);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.id);
            }
            #endregion

            return PagedList<Sys_ActivityLogMapping>.Create(query, parameters.PageIndex, parameters.Length);
        }

        /// <summary>
        /// 获取用户最新的20条操作日志
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Sys_ActivityLogMapping> GetLastUserActivityLogs(string userId)
        {
            return _dbContext.sys_activitylog.Where(o => o.creator == userId).OrderByDescending(o => o.creation_time).Take(20)
                .ToList().Select(item => new Sys_ActivityLogMapping()
                {
                    id = item.id,
                    creation_time = item.creation_time,
                    entity_name = item.entity_name,
                    method = item.method,
                    comment = item.comment,
                }).ToList();
        }

        /// <summary>
        /// 获取日志的详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_ActivityLogMapping GetActivityLogMapping(string id)
        {
            var query = from log in _dbContext.sys_activitylog.Where(o => o.id == id)
                        join u in _dbContext.sys_user on log.creator equals u.id
                        join c in _dbContext.sys_activitylog_comment on log.entity_name equals c.entity_name into temp
                        from lcomment in temp.DefaultIfEmpty()
                        select new Sys_ActivityLogMapping()
                        {
                            id = log.id,
                            entity_name = log.entity_name,
                            method = log.method,
                            newvalue = log.newvalue,
                            oldvalue = log.oldvalue,
                            creation_time = log.creation_time,
                            primary_key = log.primary_key,
                            user_name = u.name,
                            user_account = u.account,
                            comment = lcomment.comment
                        };
            return query.FirstOrDefault();
        }

        #region 私有方法

        /// <summary>
        /// 私有方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logService"></param>
        /// <param name="method"></param>
        /// <param name="entity"></param>
        public void InsertActivityLog<T>(string method, object primaryKey, string oldValue, string newValue, string userId = null)
        {
            try
            {
                var log = new Entities.sys_activitylog()
                {
                    id = CombGuid.NewGuidAsString(),
                    primary_key = primaryKey.ToString(),
                    creation_time = DateTime.Now,
                    method = method,
                    oldvalue = oldValue,
                    newvalue = newValue,
                    entity_name = typeof(T).Name,
                    creator = userId
                };
                _dbContext.sys_activitylog.Add(log);
                _dbContext.SaveChanges();
            }
            catch
            {

            }
        }


        #endregion

    }
}
