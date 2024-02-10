using Hangfire;
using Hangfire.SqlServer;
using HCE.BackgroundService.ImportFiles;
using HCE.BackgroundService.NPS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.BackgroundService.Extentions
{
    public static class ServiceRegistration
    {
        public static void QueueRecurringJob(this IApplicationBuilder app, IServiceProvider provider)
        {
            var importFileRecurringJob = provider.GetService<IImportFileRecurringJob>();
            var npsRecurringJob = provider.GetService<INpsRecurringJob>();

            importFileRecurringJob.ImportTextFile();
            importFileRecurringJob.ImportCustomerDatabaseAndReportsFile();

            npsRecurringJob.CalculateOverallNps();
        }
    }
}
