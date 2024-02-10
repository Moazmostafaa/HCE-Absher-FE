using HCE.Domain.Entities.Customers;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Services.Import;
using HCE.Interfaces.Repositories;
using HCE.Utility.CommonModels;
using HCE.Utility.Extensions;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.BackgroundService.ImportFiles
{
    public class ImportFileJobService : IImportFileJobService
    {
        private readonly ImportSetting _importSetting;
        private readonly IWriteRepository<CRMComplain> _writeRepository;
        private readonly IWriteRepository<MsOriginating> _msOriginatingWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ImportFileJobService(IOptions<ImportSetting> importSetting, IWriteRepository<CRMComplain> writeRepository, IUnitOfWork unitOfWork, IWriteRepository<MsOriginating> msOriginatingWriteRepository)
        {
            _importSetting = importSetting.Value;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _msOriginatingWriteRepository = msOriginatingWriteRepository;
        }

        public async Task ImportCustomerDatabaseAndReportsFileAsync()
        {
            try
            {
                var importService = new ImportCustomerDatabaseAndReportsService();
                var currentDirectory = Directory.GetCurrentDirectory();
                var folderPath = currentDirectory + _importSetting.ImportCustomerDatabaseAndReportsPath;
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePaths = Directory.GetFiles(folderPath).ToList();
                foreach (var filePath in filePaths)
                {
                    var excelFileData = importService.RetrieveFileData(filePath, _importSetting);

                    if (excelFileData.FileData.RawDataFromCRMComplains.Any())
                    {
                        var crmComplains = excelFileData.FileData.RawDataFromCRMComplains.AsQueryable()
                            .Select(CRMComplain.Projection).ToList();

                        await _writeRepository.AddAsync(crmComplains);
                    }
                }
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        public async Task ImportTextFileAsync()
        {
            try
            {
                var importService = new ImportTextFileService();
                var currentDirectory = Directory.GetCurrentDirectory();
                var folderPath = currentDirectory + _importSetting.ImportTextFilePath;
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePaths = Directory.GetFiles(folderPath).ToList();
                foreach (var filePath in filePaths)
                {
                    var textFileData = importService.RetrieveFileData(filePath, _importSetting);

                    if (textFileData.FileData.MSOriginatings.Any())
                    {
                        var msOriginating = textFileData.FileData.MSOriginatings.AsQueryable()
                                            .Select(MsOriginating.Projection).ToList();

                       await _msOriginatingWriteRepository.AddAsync(msOriginating);

                    }
                }
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
