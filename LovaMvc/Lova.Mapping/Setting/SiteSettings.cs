using System;
using System.Collections.Generic;
using System.Text;

namespace Lova.Mapping
{
    public class SiteSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public SiteSettings()
        {
            SiteName = "Fast管理中心";
            EmailHost = "smtp.qq.com";
            EmailAccount = "";
            EmailPassword = "587";
            ErrorToMailAddress = "";
            EmailErrorPush = "0";
            EmailPort = "";
            JPushApk = "";
            JPushSecret = "";
            CompanyName = "";
            OSSAccessKeyId = "";
            OSSAccessKeySecret = "";
        }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 邮件发送host
        /// </summary>
        public string EmailHost { get; set; }

        /// <summary>
        /// 邮件端口
        /// </summary>
        public string EmailPort { get; set; }

        /// <summary>
        /// 邮件账号
        /// </summary>
        public string EmailAccount { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string EmailPassword { get; set; }

        /// <summary>
        /// 系统错误时接收邮件地址
        /// </summary>
        public string ErrorToMailAddress { get; set; }

        /// <summary>
        /// 开启邮件错误消息提醒,1:开启，0：否
        /// </summary>
        public string EmailErrorPush { get; set; }

        /// <summary>
        /// 极光推送apk
        /// </summary>
        public string JPushApk { get; set; }

        /// <summary>
        /// 极光推送Secret
        /// </summary>
        public string JPushSecret { get; set; }

        /// <summary>
        /// 文件上传的key
        /// </summary>
        public string OSSAccessKeyId { get; set; }

        /// <summary>
        /// 文件上传的密钥
        /// </summary>
        public string OSSAccessKeySecret { get; set; }
    }
}
