using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace cts.web.core.Mail
{
    /// <summary>
    /// 邮件发送提供者
    /// </summary>
    public class MailProvide : IMailProvide
    {

        private ILogger<MailSender> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MailProvide(ILogger<MailSender> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtml"></param>
        public void Smtp(MailConfig config, string to, string subject, string body, bool isHtml = false)
        {
            Smtp(config, new List<string>() { to }, subject, body, isHtml);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="tos"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtml"></param>
        public async void Smtp(MailConfig config, IEnumerable<string> tos, string subject, string body, bool isHtml = false)
        {
            using (var client = new SmtpClient(config.Host, config.Port))
            {
                var message = new MailMessage();

                message.From = new MailAddress(config.Account);
                foreach (var mail in tos)
                {
                    if (!String.IsNullOrEmpty(mail))
                        message.To.Add(mail);
                }
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(config.Account, config.Password);
                try
                {
                    await client.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "邮件发送失败", config, tos);
                }
            }
        }
    }



}
