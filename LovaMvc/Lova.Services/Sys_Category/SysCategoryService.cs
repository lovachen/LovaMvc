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
    public class SysCategoryService
    {
        const string MODEL_ALL = "lova.sys.categories";

        private LovaDbContext _dbContext;
        private ICacheManager _cacheManager;
        private IMapper _mapper;

        public SysCategoryService(ICacheManager cacheManager,
            LovaDbContext dbContext,
            IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 获取全部并缓存
        /// </summary>
        /// <returns></returns>
        public List<Sys_CategoryMapping> GetAllCache()
        {
            return _cacheManager.Get<List<Sys_CategoryMapping>>(MODEL_ALL, () =>
            {
                return _dbContext.sys_category.ToList()
                .Select(item => _mapper.Map<Sys_CategoryMapping>(item)).ToList();
            });
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sysCategories"></param>
        public void Init(List<Entities.sys_category> sysCategories)
        {
            using (var tarns = _dbContext.Database.BeginTransaction())
            {
                var oldList = _dbContext.sys_category.ToList();
                oldList.ForEach(del =>
                {
                    var item = sysCategories.FirstOrDefault(o => o.uid == del.uid);
                    if (item == null)
                    {
                        _dbContext.Database.ExecuteSqlRaw($"DELETE FROM [Sys_Permission] WHERE [CategoryId]='{del.id}'");
                        _dbContext.sys_category.Remove(del);
                    }
                });
                sysCategories.ForEach(entity =>
                {
                    var item = oldList.FirstOrDefault(o => o.uid == entity.uid);
                    if (item == null)
                    {
                        _dbContext.sys_category.Add(entity);
                    }
                    else
                    {
                        item.route_template = entity.route_template ?? "";
                        item.name = entity.name;
                        item.code = entity.code;
                        item.father_code = entity.father_code;
                        item.target = entity.target ?? "0";
                        item.sort = entity.sort;
                        item.is_menu = entity.is_menu ?? 0;
                        item.controller = entity.controller ?? "";
                        item.Action = entity.Action ?? "";
                        item.RouteName = entity.RouteName ?? "";
                        item.IconClass = entity.IconClass ?? "";
                    }
                });
                _dbContext.SaveChanges();
                tarns.Commit();
            }
        }
    }
}
