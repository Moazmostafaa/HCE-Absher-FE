using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.BackgroundService.ImportFiles
{
    public interface IImportFileJobService
    {
        Task ImportTextFileAsync();
        Task ImportCustomerDatabaseAndReportsFileAsync();
    }
}
