using cts.web.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cts.web.core.Datatable
{
    /// <summary>
    /// json分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonSourceResult<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageList"></param>
        public JsonSourceResult(IPagedList<T> pageList)
        {
            this.data = pageList;
            this.counts = pageList.TotalCount;
            this.page = pageList.PageIndex; 
            this.totalpage = pageList.TotalPages;
            this.size = pageList.PageSize;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public IPagedList<T> data { get; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int counts { get; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }
         
        /// <summary>
        /// 总页数
        /// </summary>
        public int totalpage { get; }

        /// <summary>
        /// 页条数
        /// </summary>
        public int size { get; set; }
    }
}
