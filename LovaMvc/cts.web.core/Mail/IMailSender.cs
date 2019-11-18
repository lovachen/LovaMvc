using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core.Mail
{
    /// <summary>
    /// 邮件发送者，需要配置文件配置
    /// </summary>
    public interface IMailSender
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject">主题</param>
        /// <param name="isHtml">是否是html</param>
        /// <param name="body">内容</param>
        void Smtp(string to, string subject, string body, bool isHtml = false);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="tos">接收者集合</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isHtml">是否是html</param>
        void Smtp(IEnumerable<string> tos, string subject, string body, bool isHtml = false);

    }
}
