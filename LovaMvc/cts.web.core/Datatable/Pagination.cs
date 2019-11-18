using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace cts.web.core.Datatable
{ 
 
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArg"></typeparam>
    public class Pagination<TArg> where TArg : class
    {
        private readonly static int MIN_SIZE = 10;
        private readonly static int MAX_SIZE = 100;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="routeName"></param>
        /// <param name="param"></param>
        public Pagination(int pageIndex, int pageSize, int totalCount, string routeName, TArg param = null)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < MIN_SIZE ? MIN_SIZE : pageSize > MAX_SIZE ? MAX_SIZE : pageSize;

            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            this.HasPreviousPage = PageIndex > 1;
            this.HasNextPage = PageIndex + 1 <= TotalPages;
            this.RouteName = routeName;
            this.RouteArg = param;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="totalPages"></param>
        /// <param name="hasPreviousPage"></param>
        /// <param name="hasNextPage"></param>
        /// <param name="routeName"></param>
        /// <param name="param"></param>
        public Pagination(int pageIndex, int pageSize, int totalCount, int totalPages, bool hasPreviousPage, bool hasNextPage, string routeName, TArg param = null)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.TotalPages = totalPages;
            this.HasPreviousPage = hasPreviousPage;
            this.HasNextPage = hasNextPage;
            this.RouteName = routeName;
            this.RouteArg = param;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// 每页数
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 总记录
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// 分页数
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage { get; private set; }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage { get; private set; }

        /// <summary>
        /// 路由名称
        /// </summary>
        public string RouteName { get; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public TArg RouteArg { get; set; }
    }

    /// <summary>
    /// 页码参数
    /// </summary>
    public class Pagination: Pagination<object>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="routeName"></param>
        /// <param name="param"></param>
        public Pagination(int pageIndex, int pageSize, int totalCount, string routeName, object param = null) 
            : base(pageIndex,pageSize,totalCount,routeName,param)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="totalPages"></param>
        /// <param name="hasPreviousPage"></param>
        /// <param name="hasNextPage"></param>
        /// <param name="routeName"></param>
        /// <param name="param"></param>
        public Pagination(int pageIndex, int pageSize, int totalCount, int totalPages, bool hasPreviousPage, bool hasNextPage, string routeName, object param = null) 
            : base(pageIndex, pageSize, totalCount,totalPages,hasPreviousPage,hasNextPage, routeName, param)
        {

        }

    }

}
