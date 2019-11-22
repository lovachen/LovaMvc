using Lova.Entities;
using Lova.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json;
using cts.web.core;
using cts.web.core.Cache;

namespace Lova.Services
{
    public class BucketService : BaseService
    {
        private const string MODEL_KEY = "lova.services.buckets";
        private readonly static Object lockObj = new object();

        private ICacheManager _cacheManager;
        private LovaDbContext _dbContext;
        private IMapper _mapper;
        private ActivityLogService _activityLogService;

        public BucketService(ICacheManager cacheManager,
            LovaDbContext dbContext,
            ActivityLogService activityLogService,
            IMapper mapper)
        {
            _activityLogService = activityLogService;
            _mapper = mapper;
            _dbContext = dbContext;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 获取所有的bucket并缓存
        /// </summary>
        /// <returns></returns>
        public List<BucketMapping> AllBuckets()
        {
            return _cacheManager.Get<List<BucketMapping>>(MODEL_KEY, () =>
            {
                return _dbContext.bucket.Select(item => new BucketMapping()
                {
                    id = item.id,
                    name = item.name,
                    description = item.description,
                    creation_time = item.creation_time,
                    is_compress = item.is_compress
                }).ToList();
            });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public (bool Status, string Message) UpdateBucket(BucketMapping bucket, string userId)
        {
            var item = _dbContext.bucket.Find(bucket.id);
            if (item == null) return Fail("数据不存在");
            string oldLog = JsonConvert.SerializeObject(item);
            item.description = bucket.description;
            item.is_compress = bucket.is_compress;
            string newLog = JsonConvert.SerializeObject(item);
            _dbContext.SaveChanges();
            _activityLogService.UpdatedEntity<Entities.bucket>(item.id, oldLog, newLog, userId);
            _cacheManager.Remove(MODEL_KEY);
            return Success("修改成功");
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public (bool Status, string Message) AddBucket(Entities.bucket item)
        {
            lock (lockObj)
            {
                if (!_dbContext.bucket.Any(o => o.name == item.name))
                {
                    string newLog = JsonConvert.SerializeObject(item);
                    _dbContext.bucket.Add(item);
                    _dbContext.SaveChanges();
                    _activityLogService.InsertedEntity<Entities.bucket>(item.id, null, newLog, item.creator);
                    _cacheManager.Remove(MODEL_KEY);
                    return Success("添加成功");
                }
                return Fail("名称已存在");
            }
        }

        /// <summary>
        /// 从缓存中通过名称获取 忽略大小写
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BucketMapping GetBucketBayName(string name)
        {
            var list = AllBuckets();
            return list.FirstOrDefault(o => o.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// 删除，已经使用的无法删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public (bool Status, String Message) Delete(string id, string userId)
        {
            if (_dbContext.bucket_image.Any(o => o.bucket_id == id))
                return Fail("已使用，不能删除");
            _dbContext.Database.ExecuteSqlRaw($"DELETE FROM bucket WHERE id={id}");
            _cacheManager.Remove(MODEL_KEY);
            return Success("删除成功");
        }


        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BucketMapping GetBucketMapping(string id)
        {
            var item = _dbContext.bucket.AsNoTracking().FirstOrDefault(o => o.id == id);
            if (item == null) return null;
            return _mapper.Map<BucketMapping>(item);
        }






    }
}
