using System.Collections.Generic;
using System.Security.Principal;
using System;

namespace cts.web.core.Jwt
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// 登陆平台
        /// </summary>
        public int PrimarySid { get; set; }

        /// <summary>
        /// 用户id，加密后存储
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
         
        /// <summary>
        /// 数据
        /// </summary>
        public string UserData { get; set; }
    }
}