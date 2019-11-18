using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core
{
    /// <summary>
    /// 雪花算法，局限在于只能使用69年，机器总数不超过1024台
    /// 理论上来讲 12位 Sequence 一秒生产 4096 *1000个ID
    /// 想要延长年限就 减少 SequenceBits 和机器总数 ，当SequenceBits=8时 年限增加到 1115年 一秒生产 255*1000 个ID
    /// 所以以此8来计算
    /// </summary>
    public class IdWorker
    {
        /// <summary>
        /// 
        /// </summary>
        public const long Twepoch = 1552922013928L;//2019.01.01开始

        const int WorkerIdBits = 5;
        const int DatacenterIdBits = 5;
        const int SequenceBits = 8; //12
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        private const int WorkerIdShift = SequenceBits;
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits; 
        private const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;

        private static readonly DateTime Jan1st1970 = new DateTime
           (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private readonly object _lock = new Object();

        /// <summary>
        /// 机器id
        /// </summary>
        public long WorkerId { get; protected set; }

        /// <summary>
        /// 数据中心id
        /// </summary>
        public long DatacenterId { get; protected set; }

        /// <summary>
        /// 增长计数
        /// </summary>
        public long Sequence
        {
            get { return _sequence; }
            internal set { _sequence = value; }
        }

        /// <summary>
        /// 默认的work
        /// </summary>
        private static readonly IdWorker _worker = new IdWorker(0L, 0L);

        /// <summary>
        /// 创建WorkerId对象
        /// </summary>
        /// <param name="workerId">机器编号</param>
        /// <param name="datacenterId">数据中心编号</param>
        /// <param name="sequence"></param>
        public IdWorker(long workerId, long datacenterId, long sequence = 0L)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;

            // 检查workerid，5位最大值为31，最小0
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(String.Format("workerid 不能大于 {0} 或小于 0", MaxWorkerId));
            }
            // 检查 datacenterId，5位最大值为31，最小0
            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException(String.Format("datacenterId 不能大于 {0} 或小于 0", MaxDatacenterId));
            }
        }

        /// <summary>
        /// 获取默认的IWorker对象，以workerId=0，datacenterId=0 构建
        /// 只适合单机使用
        /// </summary>
        public static IdWorker Default => _worker;

        /// <summary>
        /// 获取新的Id
        /// </summary>
        /// <returns></returns>
        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();

                if (timestamp < _lastTimestamp)
                {
                    //时间戳毫秒数小于最后的值
                    throw new Exception(String.Format(
                        "时间倒退，拒绝在{0} milliseconds 生成id", _lastTimestamp - timestamp));
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & SequenceMask;
                    if (_sequence == 0)
                    {
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;
                var id = ((timestamp - Twepoch) << TimestampLeftShift) |
                         (DatacenterId << DatacenterIdShift) |
                         (WorkerId << WorkerIdShift) | _sequence;

                return id;
            }
        }

        /// <summary>
        /// 下一毫秒
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        /// <summary>
        /// 获取毫秒数
        /// </summary>
        /// <returns></returns>
        protected virtual long TimeGen()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }
}
