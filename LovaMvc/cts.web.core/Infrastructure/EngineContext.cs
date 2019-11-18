using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace cts.web.core
{
    /// <summary>
    /// 引擎上下文
    /// </summary>
    public class EngineContext
    {
        private static IEngine _engine;

        /// <summary>
        /// 创建引擎
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create(IEngine engine)
        {
            if (_engine == null)
                _engine = engine;
            return engine;
        }

        /// <summary>
        /// 当前引擎
        /// </summary>
        public static IEngine Current
        {
            get
            {
                return _engine;
            }
        }







    }
}
