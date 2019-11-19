using Lova.Services;
using cts.web.core.Mail;
using cts.web.core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lova.Framework.Filters
{
    public class WebExceptionAttribute : TypeFilterAttribute
    {
        private int _platform;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform">0:网页，1：api</param>
        public WebExceptionAttribute(int platform = 0) : base(typeof(ApiExceptionFilter))
        {
            _platform = platform;
            Arguments = new object[] { platform  };
        }

        private class ApiExceptionFilter : IExceptionFilter
        {
            private ILogger<ApiExceptionFilter> _logger;
            private IMailProvide _mailProvide;
            private SettingService _settingService;
            int _platform; 

            public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger,
                IMailProvide mailProvide,
                SettingService settingService,
                int platform = 0)
            {
                _logger = logger;
                _mailProvide = mailProvide;
                _settingService = settingService;
                _platform = platform; 
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void OnException(ExceptionContext context)
            {
                try
                {
                    var ex = context.Exception;
                    _logger.LogError(context.Exception, ex.Message ?? "");
                    //发送邮件提醒
                    var settings = _settingService.GetMasterSettings();
                    if (!String.IsNullOrEmpty(settings.ErrorToMailAddress))
                    {
                        string[] mails = mails = new string[] { settings.ErrorToMailAddress };
                        if (settings.ErrorToMailAddress.Contains(";"))
                        {
                            mails = settings.ErrorToMailAddress.Split(";");
                        }
                        var config = new MailConfig() { Account = settings.EmailAccount, Host = settings.EmailHost, Password = settings.EmailPassword };
                        if (int.TryParse(settings.EmailPort, out int _port))
                        {
                            config.Port = _port;
                            _mailProvide.Smtp(config, mails, $"{settings.SiteName}系统错误提醒", ex.Message + Environment.NewLine + ex.StackTrace);
                        }
                    }
                }
                catch (Exception)
                {

                }
                switch (_platform)
                {
                    //web页面
                    case 0:
                        if (context.HttpContext.Request.IsAjaxRequest())
                        {
                            context.Result = new JsonResult(new AjaxResult() { Success = false, Message = "系统错误，请稍后重试" });
                        }
                        else
                        {
                            context.Result = new ViewResult() { ViewName ="Error" };
                        }
                        break;
                    //api
                    case 1:
                        context.Result = new OkObjectResult(new ApiJsonResult() { code = 1001, msg = "系统错误，请稍后重试" });
                        break;
                }

            }



        }
    }
}
