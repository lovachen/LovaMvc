using System;
using System.Collections.Generic;
using System.Text;

namespace cts.web.core.Jwt
{
    /// <summary>
    /// jwt工厂接口
    /// </summary>
    public interface IJWTFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="jti"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        string CreateToken(User user, string jti, DateTime expire);
    }
}
