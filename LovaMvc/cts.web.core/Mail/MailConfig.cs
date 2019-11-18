using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core.Mail
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MailConfig
    {
        /// <summary>
        /// smtp 地址
        /// </summary>
        public string Host { get; set; } = "smtp.qq.com";

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 587;

        /// <summary>
        /// 账号,邮箱
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码，qq的是授权码
        /// </summary>
        public string Password { get; set; }
    }
}
