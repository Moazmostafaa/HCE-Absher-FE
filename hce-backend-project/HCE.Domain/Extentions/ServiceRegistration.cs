using HCE.Domain.Cache;
using HCE.Domain.ResponseModel;
using HCE.Domain.Services.Infrastructure;
using HCE.Interfaces.Cache;
using HCE.Interfaces.Domain.Response;
using HCE.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HCE.Domain.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped(typeof(IReadService<>), typeof(ReadServiceBase<>));
            services.AddScoped(typeof(IWriteService<>), typeof(WriteServiceBase<>));
            services.AddScoped(typeof(IResponseResult<>), typeof(ResponseResult<>));
            services.AddScoped(typeof(IPagedResponseResult<>), typeof(PagedResponseResult<>));
        }
    }
}
