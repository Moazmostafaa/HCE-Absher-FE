using HCE.Domain.Models.Import;
using HCE.Utility.CommonModels;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Services.Import
{
    public abstract class AbstractImportService
    {
        protected virtual DataSet GetExcelFileData(string filePath, ExeclFileSchema execlFileSchema, ImportSetting importSetting)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var excelPack = new ExcelPackage(filePath))
            {
                DataSet excelasDataSet = new DataSet();
                foreach (var sheet in execlFileSchema.Sheets)
                {
                    ExcelWorksheet worksheet = excelPack.Workbook.Worksheets[sheet.SheetName];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;

                    DataTable excelasTable = new DataTable(sheet.SheetClassName);
                    foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                    {
                        //Get colummn details
                        if (!string.IsNullOrEmpty(firstRowCell.Text))
                        {
                            var sheetColumn = sheet.SheetColumns.FirstOrDefault(p => p.ColumnName.Trim().ToLower() == firstRowCell.Text.Trim().ToLower());
                            if (sheetColumn != null)
                                excelasTable.Columns.Add(sheetColumn.PropertyName);
                        }
                    }

                    //Get row details
                    for (int rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = worksheet.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                        DataRow row = excelasTable.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                    excelasDataSet.Tables.Add(excelasTable);
                }
                return excelasDataSet;
                //var datasetJson = JsonConvert.SerializeObject(excelasDataSet.Tables);
                ////Get everything as generics and let end user decides on casting to required type
                //var generatedType = JsonConvert.DeserializeObject<T>(datasetJson);
                //return (T)Convert.ChangeType(generatedType, typeof(T));
            }
        }

        protected static List<T> BindList<T>(DataTable dt)
        {
            // Example 1:
            // Get private fields + non properties
            //var fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            // Example 2: Your case
            // Get all public fields
            var fields = typeof(T).GetProperties();

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                // Create the object of T
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        // Matching the columns with fields
                        if (fieldInfo.Name == dc.ColumnName)
                        {
                            // Get the value from the datatable cell
                            object value = dr[dc.ColumnName];
                            if (value == null || value == DBNull.Value)
                                fieldInfo.SetValue(ob, null);
                            else
                                fieldInfo.SetValue(ob, value);
                            break;
                        }
                    }
                }

                lst.Add(ob);
            }

            return lst;
        }

        protected virtual string GetImportTextFileSchemaPath(ImportSetting importSetting)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory + importSetting.ImportTextFileSchemaPath;
        }

        protected virtual string GetImportExcelFileSchemaPath(ImportSetting importSetting)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory + importSetting.ImportExcelFileSchemaPath;
        }
    }
}
