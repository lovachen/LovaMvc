using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cts.web.core.Datatable
{
    /// <summary>
    /// 无参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataSourceResult<T> : DataSourceResult<T, dynamic> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagedList"></param>
        /// <param name="routeName"></param>
        public DataSourceResult(IPagedList<T> pagedList, string routeName = null) : base(pagedList, routeName, null)
        {

        }

    }

   /// <summary>
   /// 带参数
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <typeparam name="A"></typeparam>
    public class DataSourceResult<T, A> : DataSourceResult<T, A, dynamic> where T : class where A : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageList"></param>
        /// <param name="param"></param>
        /// <param name="routeName"></param>
        public DataSourceResult(IPagedList<T> pageList, A param, string routeName = null) :
            base(pageList, param, null, routeName)
        {

        }
    }

    /// <summary>
    /// 带参数和一个另外一个数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="M1"></typeparam>
    public class DataSourceResult<T, A, M1> where T : class where A : class where M1 : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageList"></param>
        /// <param name="param"></param>
        /// <param name="m1"></param>
        /// <param name="routeName"></param>
        public DataSourceResult(IPagedList<T> pageList, A param = null, M1 m1 = null, string routeName = null)
        {
            this.Data = pageList;
            this.Paging = new Pagination(pageList.PageIndex, pageList.PageSize, pageList.TotalCount, pageList.TotalPages, pageList.HasPreviousPage, pageList.HasNextPage, routeName, param);
            this.Item1 = m1;
            this.Arg = param;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public IPagedList<T> Data { get; }

        /// <summary>
        /// 分页参数
        /// </summary>
        public Pagination Paging { get; }

        /// <summary>
        /// 其它数据M1
        /// </summary>
        public M1 Item1 { get; }

        /// <summary>
        /// 参数
        /// </summary>
        public A Arg { get; set; }
    }


}
