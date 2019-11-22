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
    public class BucketImageService
    {
        private readonly static Object lockObj = new object();
        private LovaDbContext _dbContext;
        private IMapper _mapper;

        public BucketImageService(LovaDbContext dbContext,
            IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitUrl"></param>
        /// <returns></returns>
        public BucketImageMapping GetByVisitUrl(string visitUrl)
        {
            return _dbContext.bucket_image.Select(item => new BucketImageMapping()
            {
                visiturl = item.visiturl,
                io_path = item.io_path,
                ext_name = item.ext_name,
            }).FirstOrDefault(o => o.visiturl == visitUrl);
        }

        /// <summary>
        /// 通过sha1获取
        /// </summary>
        /// <param name="sha1"></param>
        /// <returns></returns>
        public BucketImageMapping GetSHA1(string sha1)
        {
            return _dbContext.bucket_image.Select(item => new
            BucketImageMapping()
            {
                visiturl = item.visiturl,
                io_path = item.io_path,
                sha1 = item.sha1,
            }).FirstOrDefault(o => o.sha1 == sha1);
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="bucketImage"></param>
        public void AddImage(Entities.bucket_image bucketImage)
        {
            lock (lockObj)
            {
                if (!_dbContext.bucket_image.Any(o => o.sha1 == bucketImage.sha1))
                {
                    _dbContext.bucket_image.Add(bucketImage);
                    _dbContext.SaveChanges();
                }
            }
        }


        public IPagedList<BucketImageMapping> AdminSearch(BucketImageSearchArg arg, DataTablesParameters parameters)
        {
            var query = _dbContext.bucket_image.AsQueryable();

            #region 排序

            if (!String.IsNullOrEmpty(parameters.OrderName))
            {
                switch (parameters.OrderName)
                {
                    case "creation_time_format":
                        if (parameters.OrderDir.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                            query = query.OrderByDescending(o => o.creation_time);
                        else
                            query = query.OrderBy(o => o.creation_time);
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

            return PagedList<BucketImageMapping>.Create<Entities.bucket_image>(query, parameters.PageIndex, parameters.Length, _mapper);

        }







    }
}
