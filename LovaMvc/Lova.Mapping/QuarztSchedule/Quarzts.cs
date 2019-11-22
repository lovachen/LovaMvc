using System;
using System.Collections.Generic;
using System.Text;

namespace Lova.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class Quarzts
    {
        #region 组

        /// <summary>
        /// 
        /// </summary>
        public const string GROUP_NAME = "任务组";


        #endregion

        #region 名

        /// <summary>
        /// 
        /// </summary>
        public const string JOB_NAME = "任务X1";



        #endregion

        /// <summary>
        /// 任务初始数据
        /// </summary>
        public List<QuarztScheduleMapping> QuarztSchedules
        {
            get
            {
                List<QuarztScheduleMapping> list = new List<QuarztScheduleMapping>();
                //每天1点出发一次
                list.Add(new QuarztScheduleMapping()
                {
                    job_group = GROUP_NAME,
                    job_name = JOB_NAME,
                    cron_express = "0 0 1 * * ?",
                    run_status = (int)JobStatus.初始值,
                    start_run_time = DateTime.Now,
                    end_run_time = DateTime.MaxValue.AddDays(-1)
                }); 
                return list;
            }
        }
    }
}
