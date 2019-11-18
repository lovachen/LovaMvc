using cts.web.core.Jwt;
using cts.web.core.Librs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class JwtServiceCollectionExtensions
    {
        /// <summary>
        /// 启用Jwt验证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="hosting"></param>
        /// <param name="action"></param>
        public static void AddJwt(this IServiceCollection services, IWebHostEnvironment hosting, Action<JWTTokenOptions> action)
        {
            JWTTokenOptions _tokenOptions = new JWTTokenOptions();
            action(_tokenOptions);
            if (_tokenOptions.Key == null)
            {
                // 从文件读取密钥
                string keyDir = hosting.ContentRootPath;
                if (!EncryptorHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams))
                {
                    keyParams = EncryptorHelper.GenerateRSAKeysAndSave(keyDir);
                }
                _tokenOptions.Key = new RsaSecurityKey(keyParams);
            }
            _tokenOptions.Credentials = new SigningCredentials(_tokenOptions.Key, SecurityAlgorithms.RsaSha256Signature);
            _AddJwt(services, _tokenOptions);
        }

        /// <summary>
        /// 启用Jwt验证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="hosting"></param>
        public static void AddJwt(this IServiceCollection services, IWebHostEnvironment hosting)
        {
            // 从文件读取密钥
            string keyDir = hosting.ContentRootPath;
            if (!EncryptorHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams))
            {
                keyParams = EncryptorHelper.GenerateRSAKeysAndSave(keyDir);
            }
            JWTTokenOptions _tokenOptions = new JWTTokenOptions();
            _tokenOptions.Key = new RsaSecurityKey(keyParams); 
            _tokenOptions.Credentials = new SigningCredentials(_tokenOptions.Key, SecurityAlgorithms.RsaSha256Signature);

            _AddJwt(services, _tokenOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="_tokenOptions"></param>
        private static void _AddJwt(IServiceCollection services,JWTTokenOptions _tokenOptions)
        {
            services.AddSingleton(_tokenOptions);
            services.AddSingleton<IJWTFactory, JWTFactory>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = _tokenOptions.Key,
                    ValidAudience = _tokenOptions.Audience,
                    ValidIssuer = _tokenOptions.Issuer,
                    ValidateLifetime = true
                };
                jwtOptions.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.HttpContext.Request.Headers[_tokenOptions.TokenName];
                        return Task.CompletedTask;
                    }
                };
            });
        }

    }
}
