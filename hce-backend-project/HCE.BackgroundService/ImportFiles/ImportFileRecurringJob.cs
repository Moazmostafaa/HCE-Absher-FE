using Hangfire;
using HCE.Domain.Services.Import;
using HCE.Interfaces.Services.Import;
using HCE.Utility.CommonModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.BackgroundService.ImportFiles
{
    public class ImportFileRecurringJob : IImportFileRecurringJob
    {
        private readonly ImportSetting _importSetting;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IImportFileJobService _importFileJobService;

        public ImportFileRecurringJob(IRecurringJobManager recurringJobManager, IImportFileJobService importFileJobService, IOptions<ImportSetting> importSetting)
        {
            _importSetting = importSetting.Value;
            _recurringJobManager = recurringJobManager;
            _importFileJobService = importFileJobService;
        }

        public void ImportTextFile()
        {
            _recurringJobManager.AddOrUpdate("ImportTextFileRecurringJob", () => _importFileJobService.ImportTextFileAsync(), _importSetting.ImportTextFileHangfireCronExpression);
        }

        public void ImportCustomerDatabaseAndReportsFile()
        {
            _recurringJobManager.AddOrUpdate("ImportCustomerDatabaseAndReportsRecurringJob", () => _importFileJobService.ImportCustomerDatabaseAndReportsFileAsync(), _importSetting.ImportCustomerDatabaseAndReportsHangfireCronExpression);
        }
    }
}
