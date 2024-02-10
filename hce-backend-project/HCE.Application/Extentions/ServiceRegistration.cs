using HCE.Application.Common.Behaviours;
using HCE.Application.Managers;
using HCE.Interfaces.Managers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HCE.Application.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddMediatR(typeof(CreatePostCommandHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IOtpManager, OtpManager>();
            services.AddScoped<INpsCalculationManager, NpsCalculationManager>();
        }
    }
}
