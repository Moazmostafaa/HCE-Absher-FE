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
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HCE.Domain.Services.Import
{
    public class ImportTextFileService : AbstractImportService, IImportService<ImportResult<TTFiles>>
    {
        public ImportResult<TTFiles> RetrieveFileData(string filePath, ImportSetting importSetting)
        {
            ImportResult<TTFiles> ttFiles = new();
            ttFiles.FileData = new();
            try
            {
                string importTextFileSchemaPath = GetImportTextFileSchemaPath(importSetting);
                var importTextFileSchemaText = File.ReadAllText(importTextFileSchemaPath);
                var textFileSchemas = JsonConvert.DeserializeObject<List<TextFileSchema>>(importTextFileSchemaText);
                if (textFileSchemas != null && textFileSchemas.Count > 0)
                {
                    var targetSchema = textFileSchemas.FirstOrDefault(p => p.Code == ImportSchemasCodes.TTFiles);
                    if (targetSchema != null)
                    {
                        ttFiles.FileData.MSOriginatings = new();
                        var lines = File.ReadAllLines(filePath);
                        foreach (var record in targetSchema.Records)
                        {
                            if (record.RecordClassName == nameof(MSOriginating))
                            {
                                MSOriginating msOriginating = null;
                                for (var i = 0; i < lines.Length; i += 1)
                                {
                                    var line = lines[i];
                                    if (!(string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line) || string.IsNullOrWhiteSpace(line.Trim())))
                                    {
                                        if (line.Contains(record.RecordName))
                                        {
                                            if (msOriginating == null)
                                                msOriginating = new MSOriginating();
                                            else
                                            {
                                                ttFiles.FileData.MSOriginatings.Add(msOriginating);
                                                msOriginating = new();
                                            }
                                        }
                                        else if (msOriginating != null)
                                        {
                                            foreach (var recordColumn in record.RecordColumns)
                                            {
                                                if (line.Contains(recordColumn.ColumnName) || (!string.IsNullOrEmpty(recordColumn.ColumnName2) && line.Contains(recordColumn.ColumnName2)))
                                                {
                                                    var lineValues = Regex.Split(line.Trim(), @"\s{2,}");
                                                    SetPopertyValue(msOriginating, recordColumn.PropertyName, lineValues[1]);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (msOriginating != null)
                                {
                                    ttFiles.FileData.MSOriginatings.Add(msOriginating);
                                    msOriginating = new();
                                }
                            }
                        }
                    }
                    else
                    {
                        ttFiles.FileData = null;
                        ttFiles.IsSuccess = false;
                        return ttFiles;
                    }
                }
                else
                {
                    ttFiles.FileData = null;
                    ttFiles.IsSuccess = false;
                    return ttFiles;
                }

                ttFiles.IsSuccess = true;
                return ttFiles;
            }
            catch (Exception ex)
            {
                ttFiles.FileData = null;
                ttFiles.IsSuccess = false;
                ttFiles.ErrorMessage = ex.Message;
                return ttFiles;
            }
        }

        //private void SetPopertyValue(object instance, string propertyName, string value)
        //{
        //    Type type = instance.GetType();
        //    PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public, null, typeof(string), new Type[0], null);

        //    propertyInfo.SetValue(instance, value, null);
        //}

        private void SetPopertyValue(object instance, string propertyName, string value)
        {
            Type objectType = instance.GetType();
            objectType.GetProperty(propertyName).SetValue(instance, value);
        }
    }
}
