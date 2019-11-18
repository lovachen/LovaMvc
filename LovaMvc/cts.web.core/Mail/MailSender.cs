using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace cts.web.core.Mail
{
    /// <summary>
    /// 
    /// </summary>
    public class MailSender : IMailSender
    {
        private MailConfig _config;
        private ILogger<MailSender> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public MailSender(MailConfig config,
            ILogger<MailSender> logger)
        {
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtml"></param>
        public void Smtp(string to, string subject, string body, bool isHtml = false)
        {
            Smtp(new List<string>() { to }, subject, body, isHtml);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="tos"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtml"></param>
        public async void Smtp(IEnumerable<string> tos, string subject, string body, bool isHtml = false)
        {
            using (var client = new SmtpClient(_config.Host, _config.Port))
            {
                var message = new MailMessage();

                message.From = new MailAddress(_config.Account);
                foreach (var mail in tos)
                {
                    if(!String.IsNullOrEmpty(mail))
                        message.To.Add(mail);
                }
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_config.Account, _config.Password);
                try
                {
                    await client.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "邮件发送失败", _config, tos);
                }
            }
        }
    }
}
