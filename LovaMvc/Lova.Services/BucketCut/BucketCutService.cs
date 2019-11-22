using AutoMapper;
using cts.web.core.Cache;
using Lova.Entities;
using Lova.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lova.Services
{
    public class BucketCutService
    {
        private const string MODEL_KEY = "lova.services.bucketcuts";

        private ICacheManager _cacheManager;
        private LovaDbContext _dbContext;
        private IMapper _mapper;

        public BucketCutService(ICacheManager cacheManager,
            LovaDbContext dbContext,
            IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCut(Guid id)
        {
            var item = _dbContext.bucket_cut.Find(id);
            if (item != null)
                _dbContext.Remove(item);
            _dbContext.SaveChanges();
            _cacheManager.Remove(MODEL_KEY);
        }

        /// <summary>
        /// 获取所有数据并缓存
        /// </summary>
        /// <returns></returns>
        public List<BucketCutMapping> AllBucketCuts()
        {
            return _cacheManager.Get<List<BucketCutMapping>>(MODEL_KEY, 3600, () =>
            {
                return _dbContext.bucket_cut
                .Select(item => new BucketCutMapping()
                {
                    id = item.id,
                    bucket_id = item.bucket_id,
                    value = item.value
                }).ToList();
            });
        }


        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BucketCutMapping GetById(string id)
        {
            var list = AllBucketCuts();
            if (list == null) return null;
            return list.FirstOrDefault(o => o.id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void InsertCut(Entities.bucket_cut entity)
        {
            if (_dbContext.bucket_cut.Any(o => o.bucket_id == entity.bucket_id && o.value == entity.value))
                return;
            _dbContext.bucket_cut.Add(entity);
            _dbContext.SaveChanges();
            _cacheManager.Remove(MODEL_KEY);
        }


        /// <summary>
        /// 检查剪裁尺长是否存在
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ValueExists(string bucket, string value)
        {
            var list = AllBucketCuts();
            return list != null && list.Any(o => o.value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
