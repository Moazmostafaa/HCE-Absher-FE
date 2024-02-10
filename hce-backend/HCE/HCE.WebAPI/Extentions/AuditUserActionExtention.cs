using HCE.Domain.Entities.Audit;
using HCE.Domain.Entities.TablesSchema;
using HCE.Interfaces.Domain;
using HCE.Interfaces.UserResolverHandler;
using Audit.WebApi;
using Audit.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audit.Core;
using HCE.Utility.Extensions;

namespace HCE.WebAPI.Extentions
{
    public static class AuditUserActionExtention
    {
        public static void AdAuditUserAction(this IServiceCollection services, IConfiguration configuration)
        {
            var sp = services.BuildServiceProvider();
            var userResolverHandler = sp.GetService<IUserResolverHandler>();

            Audit.Core.Configuration.Setup()
                    .UseSqlServer(config => config
                    .ConnectionString(configuration.GetConnectionString("HCEDbContextConnection"))
                    .Schema(TablesSchema.AuditSchema)
                    .TableName(nameof(AuditUserAction))
                    .JsonColumnName(nameof(AuditUserAction.JsonData))
                    .IdColumnName(nameof(AuditUserAction.AuditUserActionId))
                    .LastUpdatedColumnName(nameof(AuditUserAction.UpdatedDate))
                    .CustomColumn(nameof(AuditUserAction.EventType), ev => ev.EventType)
                    .CustomColumn(nameof(AuditUserAction.CreatedBy), ev => userResolverHandler.GetUserId())
                    .CustomColumn(nameof(AuditUserAction.UpdatedBy), ev => userResolverHandler.GetUserId())
                    .CustomColumn(nameof(AuditUserAction.CreatedDate), ev => DateTime.Now.GetCurrentDateTime())
                );

            services.AddMvc(mvc =>
            {
                mvc.Filters.Add(new AuditIgnoreActionFilter());
            });
        }
    }
}
