using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lova.Mapping;
using cts.web.core;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;

namespace Lova.Services
{
    public class JobCenter : BaseService
    {
        private readonly ILogger<QuartzStartup> _logger;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _iocJobfactory;
        private IScheduler _scheduler;

        public JobCenter(IJobFactory iocJobfactory, ILogger<QuartzStartup> logger, ISchedulerFactory schedulerFactory)
        {
            this._logger = logger;
            //1、声明一个调度工厂
            this._schedulerFactory = schedulerFactory;
            this._iocJobfactory = iocJobfactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IScheduler> GetSchedulerAsync()
        {
            if (_scheduler != null)
                return _scheduler;
            _scheduler = await _schedulerFactory.GetScheduler();
            _scheduler.JobFactory = this._iocJobfactory;
            return _scheduler;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public async Task<(bool Status, string Message)> AddScheduleJobAsync(QuarztScheduleMapping m)
        {
            try
            {
                if (m == null)
                {
                    return Fail("model 为空");
                }

                DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(m.start_run_time, 1);
                DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(m.end_run_time, 1);
                //2、通过调度工厂获得调度器
                var scheduler = await GetSchedulerAsync();

                //创建一个触发器
                ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                     .StartAt(starRunTime)
                                                     .EndAt(endRunTime)
                                                     .WithIdentity(m.job_name, m.job_group)
                                                     .WithCronSchedule(m.cron_express)
                                                     .Build();
                //创建任务
                IJobDetail jobDetail = JobBuilder.Create<CustomJob>()
                           .WithIdentity(m.job_name, m.job_group)
                           .Build();
                //将触发器和任务器绑定到调度器中
                await scheduler.ScheduleJob(jobDetail, trigger);
                await scheduler.Start();
                return Success("添加任务成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"添加任务失败");
                return Fail($"添加任务失败。{ex.Message}");
            }
        }


        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="job_group"></param>
        /// <param name="job_name"></param>
        /// <returns></returns>
        public async Task<(bool Status, string Message)> StopScheduleJobAsync(string job_group, string job_name)
        {
            try
            {
                var scheduler = await GetSchedulerAsync();
                await scheduler.PauseJob(new JobKey(job_name, job_group));
                return Success("暂停成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务暂停失败");
                return Fail($"任务暂停失败。{ex.Message}");
            }
        }

        /// <summary>
        /// 恢复指定的任务计划**恢复的是暂停后的任务计划，如果是程序奔溃后 或者是进程杀死后的恢复，此方法无效
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        public async Task<(bool Status, string Message)> RunScheduleJobAsync(QuarztScheduleMapping sm)
        {
            try
            {
                var scheduler = await GetSchedulerAsync();
                await scheduler.ResumeJob(new JobKey(sm.job_name, sm.job_group));
                return Success("任务恢复成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务恢复失败");
                return Fail($"任务恢复失败。{ex.Message}");
            }
        }
    }
}
