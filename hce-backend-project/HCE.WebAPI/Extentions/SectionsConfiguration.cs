using HCE.Utility.CommonModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCE.WebAPI.Extentions
{
    public static class SectionsConfiguration
    {
        public static void AddSectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSetting>(configuration.GetSection("AppSetting"));
            var appSettings = configuration.GetSection("AppSetting").Get<AppSetting>();
            services.AddSingleton(appSettings);

            services.Configure<RedisSetting>(configuration.GetSection("RedisSetting"));
            var redisSettings = configuration.GetSection("RedisSetting").Get<RedisSetting>();
            services.AddSingleton(redisSettings);
            
            services.Configure<AppOtpSettings>(configuration.GetSection("AppOtpSettings"));
            var otpSettings = configuration.GetSection("AppOtpSettings").Get<AppOtpSettings>();
            services.AddSingleton(otpSettings);

            services.Configure<ImportSetting>(configuration.GetSection("ImportSetting"));
            var importSetting = configuration.GetSection("ImportSetting").Get<ImportSetting>();
            services.AddSingleton(importSetting);
        }
    }
}
