using HCE.Interfaces.DBContext;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Persistence.Configuration;
using HCE.Persistence.DBContext;
using HCE.Persistence.Repositories.Blob;
using HCE.Persistence.Repositories.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HCE.Persistence.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            #region Add DbContext
            services.AddDbContext<HCEDbContext>(options =>
               options.UseSqlServer(GetHCEDbContextConnectionString(configuration),
                   b => b.MigrationsAssembly(typeof(HCEDbContext).Assembly.FullName)));
            services.AddScoped<DbContext, HCEDbContext>();
            services.AddScoped<IHCEDbContext>(provider => provider.GetService<HCEDbContext>());
            #endregion

            #region Add Repository
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepositoryBase<>));
            services.AddScoped(typeof(IReadBlobRepository), typeof(ReadBlobRepository));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepositoryBase<>));
            services.AddScoped(typeof(IWriteBlobRepository), typeof(WriteBlobRepository));
            #endregion

            #region Add UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Add Http Context Accessor
            services.AddHttpContextAccessor();
            services.AddScoped<IUserResolverHandler, UserResolverHandler.UserResolverHandler>();
            #endregion
        }

        public static void UseAutoMigrateDatabase<TDbContext>(this IApplicationBuilder builder)
            where TDbContext : DbContext
        {
            using (var serviceScope =
                builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TDbContext>().Database.Migrate();
            }
        }

        private static string GetHCEDbContextConnectionString(IConfiguration configuration)
        {
            return new DatabaseConfiguration(configuration).GetHCEDbContextConnectionString();
        }
    }
}
