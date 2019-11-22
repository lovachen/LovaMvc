using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lova.Entities;
using Lova.Mapping;
using AutoMapper;
using System.Linq;

namespace Lova.Services
{
    public class QuartzStartup
    {
        private JobCenter _jobCenter;
        private LovaDbContext _dbContext;
        private IMapper _mapper;

        public QuartzStartup(JobCenter jobCenter,
            IMapper mapper,
            LovaDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _jobCenter = jobCenter;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            //执行中的任务
            var jobList = _dbContext.quarzt_schedule.Where(o => o.run_status == (int)JobStatus.执行任务中).ToList()
                 .Select(item => _mapper.Map<QuarztScheduleMapping>(item)).ToList();
            jobList.ForEach(async item =>
            {
                await _jobCenter.AddScheduleJobAsync(item);
            });
            await Task.FromResult(0);
        }


    }
}
