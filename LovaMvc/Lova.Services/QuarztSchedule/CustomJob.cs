using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lova.Mapping;
using Lova.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using cts.web.core.Librs;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Lova.Services
{
    /// <summary>
    /// 自定义任务
    /// </summary>
    public class CustomJob : IJob
    {
        private static readonly object lockFwwObj = new object();
        private static readonly object lockHotObj = new object();
        private static readonly object lockOAEmpObj = new object();

        private IServiceProvider _serviceProvider;

        public CustomJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                var name = context.JobDetail.Key.Name;
                var group = context.JobDetail.Key.Group;
                if (group == "" && name == "")
                {

                }
                 
            });
        }
 
    }
}
