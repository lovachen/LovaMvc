using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cts.web.core;

namespace cts.web.core.Datatable
{
    /// <summary>
    /// 
    /// </summary>
    public static class IPageListExtensions
    {

        /// <summary>
        /// 不带参数
        /// </summary>
        /// <param name="pageList"></param>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public static DataSourceResult<T> ToDataResult<T>(this IPagedList<T> pageList, string routeName = null) where T : class
        {
            return new DataSourceResult<T>(pageList, routeName);
        }

        /// <summary>
        /// 带参数
        /// </summary>
        /// <param name="pageList"></param>
        /// <param name="routeName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataSourceResult<T, A> ToDataResult<T, A>(this IPagedList<T> pageList, A param, string routeName = null) where T : class where A : class
        {
            return new DataSourceResult<T, A>(pageList, param, routeName);
        }

        /// <summary>
        /// 带参数和一个其他数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="M1"></typeparam>
        /// <param name="pageList"></param>
        /// <param name="param"></param>
        /// <param name="m1"></param>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public static DataSourceResult<T, A, M1> ToDataResult<T, A, M1>(this IPagedList<T> pageList, A param, M1 m1, string routeName = null) where T : class where A : class where M1 : class
        {
            return new DataSourceResult<T, A, M1>(pageList, param, m1, routeName);
        }

        /// <summary>
        /// app json格式数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageList"></param>
        /// <returns></returns>
        public static JsonSourceResult<T> ToJsonResult<T>(this IPagedList<T> pageList) where T : class
        {
            return new JsonSourceResult<T>(pageList);
        }

        /// <summary>
        /// 转成 jquery.datatable数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageList"></param>
        /// <returns></returns>
        public static DatatableModel<T> ToAjax<T>(this IPagedList<T> pageList)
        {
            DatatableModel<T> model = new DatatableModel<T>(pageList);
            return model;
        }
    }
}
