using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lova.Services
{
    public class IOCJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public IOCJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 新任务
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="job"></param>
        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
