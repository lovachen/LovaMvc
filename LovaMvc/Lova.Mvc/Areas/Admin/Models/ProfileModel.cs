using Lova.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lova.Mvc.Areas.Admin.Models
{
    public class ProfileModel
    {
        public ProfileModel()
        {
            ChangePassword = new ChangePasswordModel();
        }

        /// <summary>
        /// 用户
        /// </summary>
        public Sys_UserMapping User { get; set; }

        /// <summary>
        /// 登录日记
        /// </summary>
        public IEnumerable<Sys_UserLoginMapping> LoginLogList { get; set; }

        /// <summary>
        /// 修改密码对象
        /// </summary>
        public ChangePasswordModel ChangePassword { get; set; }
    }
}
