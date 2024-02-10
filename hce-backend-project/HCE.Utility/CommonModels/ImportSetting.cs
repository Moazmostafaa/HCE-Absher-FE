using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.CommonModels
{
    public class ImportSetting
    {
        public string ImportTextFilePath { get; set; }
        public string ImportTextFileSchemaPath { get; set; }
        public string ImportExcelFileSchemaPath { get; set; }
        public string ImportCustomerDatabaseAndReportsPath { get; set; }
        public string ImportTextFileHangfireCronExpression { get; set; }
        public string ImportCustomerDatabaseAndReportsHangfireCronExpression { get; set; }
    }
}
