using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCE.WebAPI.Extentions
{
    public static class RedisCacheSetup
    {
        public static void AddRedisCacheSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration =
                    $"{configuration.GetValue<string>("RedisSettings:Server")}:{configuration.GetValue<int>("RedisSettings:Port")}";
            });
        }
    }
}
