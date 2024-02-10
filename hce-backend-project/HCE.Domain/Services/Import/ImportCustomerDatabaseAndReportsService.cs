using HCE.Domain.Constants;
using HCE.Domain.Models.Import;
using HCE.Interfaces.Models.Import;
using HCE.Interfaces.Services.Import;
using HCE.Utility.CommonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Services.Import
{
    public class ImportCustomerDatabaseAndReportsService : AbstractImportService, IImportService<ImportResult<CustomerDatabaseAndReports>>
    {
        public ImportResult<CustomerDatabaseAndReports> RetrieveFileData(string filePath, ImportSetting importSetting)
        {
            ImportResult<CustomerDatabaseAndReports> customerDatabaseAndReports = new();
            customerDatabaseAndReports.FileData = new();
            try
            {
                string importFileSchemaPath = GetImportExcelFileSchemaPath(importSetting);
                var importExcelFileSchemaText = File.ReadAllText(importFileSchemaPath);
                var execlFileSchemas = JsonConvert.DeserializeObject<List<ExeclFileSchema>>(importExcelFileSchemaText);
                if (execlFileSchemas != null && execlFileSchemas.Count > 0)
                {
                    var targetSchema = execlFileSchemas.FirstOrDefault(p => p.Code == ImportSchemasCodes.CustomerDatabaseAndReports);
                    if (targetSchema != null)
                    {
                        var dataSet = GetExcelFileData(filePath, targetSchema, importSetting);
                        foreach (var table in dataSet.Tables)
                        {
                            if (((DataTable)table).TableName == nameof(RawDataFromCRMComplains))
                                customerDatabaseAndReports.FileData.RawDataFromCRMComplains = BindList<RawDataFromCRMComplains>(((DataTable)table));
                        }
                    }
                    else
                    {
                        customerDatabaseAndReports.FileData = null;
                        customerDatabaseAndReports.IsSuccess = false;
                        //customerDatabaseAndReports.ErrorMessage = ex.Message;
                        return customerDatabaseAndReports;
                    }
                }
                else
                {
                    customerDatabaseAndReports.FileData = null;
                    customerDatabaseAndReports.IsSuccess = false;
                    //customerDatabaseAndReports.ErrorMessage = ex.Message;
                    return customerDatabaseAndReports;
                }

                customerDatabaseAndReports.IsSuccess = true;
                return customerDatabaseAndReports;
            }
            catch (Exception ex)
            {
                customerDatabaseAndReports.FileData = null;
                customerDatabaseAndReports.IsSuccess = false;
                customerDatabaseAndReports.ErrorMessage = ex.Message;
                return customerDatabaseAndReports;
            }
        }

        //public T RetrieveExcelFileData<T>(string filePath)
        //{
        //    ImportResult<CustomerDatabaseAndReports> customerDatabaseAndReports = new();
        //    var execlFileSchemas = JsonConvert.DeserializeObject<List<ExeclFileSchema>>(File.ReadAllText(filePath));
        //    if (execlFileSchemas != null && execlFileSchemas.Count > 0)
        //    {
        //        var targetSchema = execlFileSchemas.FirstOrDefault(p => p.Code == ImportSchemasCodes.CustomerDatabaseAndReports);
        //    }
        //    return (T)Convert.ChangeType(customerDatabaseAndReports, typeof(T));
        //}
    }
}
