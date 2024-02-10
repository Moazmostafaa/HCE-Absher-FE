using HCE.Domain.Entities.Common.Enum;
using HCE.Domain.Entities.General;
using HCE.Interfaces.Models.Dto.User.UserAttachment;
using HCE.Interfaces.Repositories;
using HCE.Persistence.Repositories.Infrastructure;
using HCE.Utility.CommonModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Persistence.Repositories.Blob
{
    public interface IWriteBlobRepository : IWriteRepository<Attachment>
    {
        public Task Delete(Guid attachmentId);
        public Task Delete(string fileName);
        public Task<Guid> PersistAsync(Stream stream, string fileName, int moduleId, CancellationToken cancellationToken = new());
    }

    public class WriteBlobRepository : WriteRepositoryBase<Attachment>, IWriteBlobRepository
    {
        private readonly string _blobDirectory;
        private readonly string _blobBaseDirectory;

        private readonly string userAttachmentServerURL;
        private readonly string userAttachmentDirectory;

        public WriteBlobRepository(IOptions<AppSetting> options, IHostEnvironment hostEnvironment, DbContext dataBaseContext)
            : base(dataBaseContext)
        {
            _blobDirectory = options.Value.BlobDirectory;
            _blobBaseDirectory = hostEnvironment.ContentRootPath;
        }

        public async Task<Guid> PersistAsync(Stream stream, string fileName, int moduleId, CancellationToken cancellationToken)
        {
            string targetPath;
            string targetServerURL;
            var newFileName = $"{DateTime.Now:ddMMyyyyHHmmss}_{fileName}";

            GetTargetPath(moduleId, out targetPath, out targetServerURL);

            string filePathWithoutFileName = string.Format("{0}\\{1}\\{2}\\{3}", targetPath, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string fullFilePath = string.Format("{0}{1}", targetServerURL, filePathWithoutFileName);

            if (!Directory.Exists(fullFilePath))
                Directory.CreateDirectory(fullFilePath);

            fullFilePath = string.Format("{0}{1}\\{2}", targetServerURL, filePathWithoutFileName, newFileName);
            await using var fileStream = new FileStream($"{fullFilePath}", FileMode.Create);
            await stream.CopyToAsync(fileStream, cancellationToken);

            var filePath = string.Format("{0}\\{1}", filePathWithoutFileName, newFileName);
            Attachment attachment = new()
            {
              //  AttachmentId = Guid.NewGuid(),
                FileName = fileName,
                FilePath = filePath,
                ModuleId = moduleId,
                SizeByByte = stream.Length,
                Extention = Path.GetExtension(fileName)
            };

            Add(attachment);
            return attachment.AttachmentId;
        }

        public Task Delete(Guid attachmentId) =>
            Task.Run(() =>
            {
                var attachment = dbSet.FirstOrDefault(p => p.AttachmentId == attachmentId);
                Delete(attachment);
                DeleteFile(attachment);
            });

        public Task Delete(string fileName) =>
            Task.Run(() =>
            {
                var attachment = dbSet.FirstOrDefault(p => p.FileName == fileName);
                Delete(attachment);

                DeleteFile(attachment);
            });

        public override void Delete(Expression<Func<Attachment, bool>> predicate)
        {
            IEnumerable<Attachment> attachments = dbSet.Where(predicate).AsEnumerable();
            foreach (var item in attachments)
            {
                base.Delete(item);
                DeleteFile(item);
            }
        }

        public override void Delete(Attachment entity)
        {
            base.Delete(entity);
            DeleteFile(entity);
        }

        private void DeleteFile(Attachment attachment)
        {
            string targetPath;
            string targetServerURL;
            GetTargetPath(attachment.ModuleId, out targetPath, out targetServerURL);
            var file = $"{targetServerURL}//{attachment.FilePath}";
            if (File.Exists(file))
                File.Delete(file);
        }

        private void GetTargetPath(int moduleId, out string targetPath, out string targetServerURL)
        {
            switch ((ModuleEnum)moduleId)
            {
                case ModuleEnum.User:
                    targetPath = userAttachmentDirectory;
                    targetServerURL = userAttachmentServerURL;
                    break;
                default:
                    targetPath = null;
                    targetServerURL = null;
                    break;
            }

            if (string.IsNullOrEmpty(targetServerURL) || string.IsNullOrWhiteSpace(targetServerURL))
                targetServerURL = _blobBaseDirectory;
            if (string.IsNullOrEmpty(targetPath) || string.IsNullOrWhiteSpace(targetPath))
                targetServerURL = _blobDirectory;
        }
    }
}
