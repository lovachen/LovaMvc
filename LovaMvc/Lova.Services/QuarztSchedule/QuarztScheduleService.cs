using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Lova.Entities;
using Lova.Mapping;
using cts.web.core;

namespace Lova.Services
{
    public class QuarztScheduleService : BaseService
    {
        private LovaDbContext _dbContext;
        private IMapper _mapper;
        private JobCenter _jobCenter;

        public QuarztScheduleService(LovaDbContext dbContext,
            IMapper mapper,
            JobCenter jobCenter)
        {
            _jobCenter = jobCenter;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// 获取所有任务调度
        /// </summary>
        /// <returns></returns>
        public List<QuarztScheduleMapping> GetTaskList()
        {
            return _dbContext.quarzt_schedule.ToList()
                .Select(item => _mapper.Map<QuarztScheduleMapping>(item)).OrderBy(o => o.job_group).ThenBy(o => o.job_name).ToList();
        }

        /// <summary>
        /// 开启任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (bool Status, string Message) Start(Guid id)
        {
            var item = _dbContext.quarzt_schedule.Find(id);
            if (item == null)
                return Fail("任务不存在");

            var res = _jobCenter.AddScheduleJobAsync(_mapper.Map<QuarztScheduleMapping>(item)).Result;
            if (res.Status)
            {
                item.run_status = (int)JobStatus.执行任务中;
                _dbContext.SaveChanges();
            }
            return res;
        }


        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (bool Status, string Message) Stop(Guid id)
        {
            var item = _dbContext.quarzt_schedule.Find(id);
            if (item == null)
                return Fail("任务不存在");
            var res = _jobCenter.StopScheduleJobAsync(item.job_group, item.job_name).Result;
            if (res.Status)
            {
                item.run_status = (int)JobStatus.暂停任务中;
                _dbContext.SaveChanges();
            }
            return res;
        }


        /// <summary>
        /// 初始化任务，讲任务写入数据库
        /// </summary>
        public void Init()
        {
            var oldList = _dbContext.quarzt_schedule.ToList();
            var schedules = new Quarzts().QuarztSchedules;
            foreach (var del in oldList)
            {
                if (!schedules.Any(o => o.job_name == del.job_name && o.job_group == del.job_group))
                {
                    _dbContext.quarzt_schedule.Remove(del);
                }
            }
            schedules.ForEach(item =>
            {
                if (!oldList.Any(o => o.job_name == item.job_name && o.job_group == item.job_group))
                {
                    var entity = _mapper.Map<Entities.quarzt_schedule>(item);
                    entity.id = CombGuid.NewGuidAsString();
                    _dbContext.quarzt_schedule.Add(entity);
                }
            });
            _dbContext.SaveChanges();
        }


    }
}
