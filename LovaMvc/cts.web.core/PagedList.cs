using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cts.web.core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        private readonly static int MIN_SIZE = 10;
        private readonly static int MAX_SIZE = 100;

        /// <summary>
        /// 
        /// </summary>
        public PagedList()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < MIN_SIZE ? MIN_SIZE : pageSize > MAX_SIZE ? MAX_SIZE : pageSize;
            this.TotalCount = source.Count();
            this.TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;

            this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList(IList<T> source, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < MIN_SIZE ? MIN_SIZE : pageSize > MAX_SIZE ? MAX_SIZE : pageSize;
            this.TotalCount = source.Count();
            this.TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;

            this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList(IList<T> data,int totalCount,int pageIndex,int pageSize)
        {
            this.TotalCount = totalCount;
            this.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(data);
        }

        /// <summary>
        /// 创建分页数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedList<T> Create(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }

        /// <summary>
        /// 创建分页数据 自动转换
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static PagedList<T> Create<E>(IQueryable<E> source,int pageIndex,int pageSize,IMapper mapper)
        {
            var count = source.Count();
            var items = source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
            var datas = items.Select(item => mapper.Map<T>(item)).ToList();
            return new PagedList<T>(datas, count, pageIndex, pageSize);
        }


        /// <summary>
        /// 
        /// </summary>
        public bool HasNextPage
        {
            get { return (PageIndex + 1 <= TotalPages); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasPreviousPage
        {
            get { return (PageIndex > 1); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PageIndex
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalPages
        {
            get;
        }
    }
}
