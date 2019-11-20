using cts.web.core;
using cts.web.core.Cache;
using Lova.Entities;
using Lova.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Lova.Services
{
    public class SettingService : BaseService
    {
        private const string MODEL_KEY = "lova.services.settings";

        private ICacheManager _cacheManager;
        private LovaDbContext _dbContext;

        public SettingService(ICacheManager cacheManager,
            LovaDbContext dbContext)
        {
            _dbContext = dbContext;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetSiteSettings()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var siteList = _dbContext.sys_setting.ToList();
            siteList.ForEach(item =>
            {
                dic.TryAdd(item.name, String.IsNullOrEmpty(item.value) ? "" : item.value.Trim());
            });
            return dic;
        }

        /// <summary>
        /// 保存基数设置的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public (bool Status, string Message) SaveSetting(string name, string value)
        {
            var item = _dbContext.sys_setting.FirstOrDefault(o => o.name == name);
            if (item == null)
            {
                _dbContext.sys_setting.Add(new Entities.sys_setting()
                {
                    id = CombGuid.NewGuidAsString(),
                    name = name,
                    value = value
                });
            }
            else
            {
                item.value = value;
            }
            _dbContext.SaveChanges();
            _cacheManager.Remove(MODEL_KEY);
            return Success("保存成功");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SiteSettings GetMasterSettings()
        {
            return _cacheManager.Get<SiteSettings>(MODEL_KEY, () =>
            {
                var jsonStr = JsonSerializer.Serialize(GetSiteSettings()); 
                return JsonSerializer.Deserialize<SiteSettings>(jsonStr);
            });
        }

        /// <summary>
        /// 初始化配置参数
        /// </summary>
        public void Init()
        {
            SiteSettings settings = new SiteSettings();
            List<Entities.sys_setting> listSettings = new List<sys_setting>();
            var propertyInfos = settings.GetType().GetProperties();
            if (propertyInfos.Length > 0)
            {
                foreach (PropertyInfo info in propertyInfos)
                {
                    if (info.CanWrite)
                    {
                        listSettings.Add(new sys_setting()
                        {
                            name = info.Name,
                            value = info.GetValue(settings)?.ToString() ?? ""
                        });
                    }
                }
            }
            if (listSettings.Any())
            {
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                var list = _dbContext.sys_setting.ToList();
                stopwatch.Stop();
                foreach (var item in list)
                {
                    if (!listSettings.Any(o => o.name == item.name))
                    {
                        _dbContext.sys_setting.Remove(item);
                    }
                }
                listSettings.ForEach(item =>
                {
                    if (!list.Any(o => o.name == item.name))
                    {
                        item.id = CombGuid.NewGuidAsString();
                        _dbContext.sys_setting.Add(item);
                    }
                });
                _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.Database.ExecuteSqlRaw("DELETE FROM [Sys_Setting]");
            }
        }
    }
}
