using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cts.web.core.Jwt
{
    /// <summary>
    /// 
    /// </summary>
    public class JWTTokenOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; } = "cts.audience";

        /// <summary>
        /// 
        /// </summary>
        public RsaSecurityKey Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SigningCredentials Credentials { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; set; } = "cts.issuer";

        /// <summary>
        /// header接口的头属性
        /// </summary>
        public string TokenName { get; set; } = "token";
    }
}
