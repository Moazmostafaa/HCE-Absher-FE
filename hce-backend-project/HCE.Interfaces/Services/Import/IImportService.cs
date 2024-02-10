using HCE.Interfaces.Models.Import;
using HCE.Utility.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Services.Import
{
    public interface IImportService<T> where T : class
    {
        T RetrieveFileData(string filePath, ImportSetting importSetting);
    }
}
