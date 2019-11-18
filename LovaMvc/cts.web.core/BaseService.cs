using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core
{
    /// <summary>
    /// 服务层基础类
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public (bool Status, string Message) Fail(string msg)
        {
            return (false, msg ?? "操作失败");
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public (bool Status, string Message) Success(string msg)
        {
            return (true, msg ?? "保存成功");
        }

    }
}
