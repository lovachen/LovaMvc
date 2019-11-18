using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace cts.web.core.Jwt
{
    /// <summary>
    /// 
    /// </summary>
    public class JWTFactory : IJWTFactory
    {
        private JWTTokenOptions _tokenOptions;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jWTTokenOptions"></param>
        public JWTFactory(JWTTokenOptions jWTTokenOptions)
        {
            _tokenOptions = jWTTokenOptions;
        }

        /// <summary>
        /// 生成一个新的 Token
        /// ClaimTypes.Name 获得 UserName
        /// ClaimTypes.UserData 获得 UserData
        /// ClaimTypes.Sid 获得 UserID
        /// JwtRegisteredClaimNames.Jti 获得 jti
        /// </summary>
        /// <param name="user">用户信息实体</param>
        /// <param name="expire">token 过期时间</param>
        /// <param name="jti"></param>
        /// <returns></returns>
        public string CreateToken(User user, string jti, DateTime expire)
        {
            var handler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
                new Claim(ClaimTypes.PrimarySid,user.PrimarySid.ToString()),
                new Claim(ClaimTypes.Sid, user.UserID??""),
                new Claim(ClaimTypes.Name,user.UserName??""),
                new Claim(ClaimTypes.UserData, user.UserData??""),
                new Claim(JwtRegisteredClaimNames.Jti,jti,ClaimValueTypes.String) // jti，用来标识 token
            };
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.UserName, "TokenAuth"), claims);
            var token = handler.CreateEncodedJwt(new SecurityTokenDescriptor
            {
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience,
                SigningCredentials = _tokenOptions.Credentials,
                Subject = identity,
                Expires = expire
            });
            return token;
        }
    }
}
