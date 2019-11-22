using Lova.Entities;
using Lova.Mapping;
using cts.web.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using cts.web.core.Datatable;

namespace Lova.Services
{

    public class SysNLogService
    {
        LovaDbContext _dbContext;
        IMapper _mapper;

        public SysNLogService(LovaDbContext dbContext,
            IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IPagedList<Sys_NLogMapping> AdminSearch(NLogSearchArg arg, DataTablesParameters parameters)
        {
            var query = _dbContext.sys_nlog.AsQueryable();

            #region 排序

            if (!String.IsNullOrEmpty(parameters.OrderName))
            {
                switch (parameters.OrderName)
                {
                    case "level":
                        if (parameters.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.level);
                        else
                            query = query.OrderBy(o => o.level);
                        break;
                    case "logged_format":
                        if (parameters.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.logged);
                        else
                            query = query.OrderBy(o => o.logged);
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

            var data = query.Select(item => new Sys_NLogMapping()
            {
                id = item.id,
                logged = item.logged,
                level = item.level,
                message = item.message
            });
            return PagedList<Sys_NLogMapping>.Create(data, parameters.PageIndex, parameters.Length);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_NLogMapping GetNlog(int id)
        {
            var item = _dbContext.sys_nlog.FirstOrDefault(o => o.id == id);
            return item != null ? _mapper.Map<Sys_NLogMapping>(item) : null;
        }

    }
}
