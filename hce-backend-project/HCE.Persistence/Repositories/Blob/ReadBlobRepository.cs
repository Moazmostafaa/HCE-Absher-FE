using HCE.Domain.Entities.Common.Enum;
using HCE.Domain.Entities.General;
using HCE.Interfaces.Models.Dto.AdminUser;
using HCE.Interfaces.Models.Dto.User.UserAttachment;
using HCE.Interfaces.Repositories;
using HCE.Persistence.Repositories.Infrastructure;
using HCE.Utility.CommonModels;
using EntityFrameworkCore.MemoryJoin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Persistence.Repositories.Blob
{
    public interface IReadBlobRepository : IReadRepository<Attachment>
    {
        FileStream GetFileStream(string fileName);
        FileStream GetFileStream(Guid attachmentId);
        FileStream GetFileStream(Attachment attachment);
        string GetFilePath(string fileName);
        string GetFilePath(Guid attachmentId);
        string GetFilePath(Attachment attachment);

        string GetFileExtension(string fileName);
        string GetFileExtension(Guid attachmentId);

        AttachmentDto GetAttachment(string fileName);
        AttachmentDto GetAttachment(Guid attachmentId);
        AttachmentDto GetAttachment(Attachment attachment);
        List<AttachmentDto> GetAttachment(List<Guid> attachmentsIds);

        byte[] GetFileBytes(string fileName);
        byte[] GetFileBytes(Guid attachmentId);
        byte[] GetFileBytes(Attachment attachment);
    }

    public class ReadBlobRepository : ReadRepositoryBase<Attachment>, IReadBlobRepository
    {
        private readonly string _blobBaseDirectory;
        private readonly string userAttachmentServerURL;

        public ReadBlobRepository(IOptions<AppSetting> options, IHostEnvironment hostEnvironment, DbContext dataBaseContext)
            : base(dataBaseContext)
        {
            _blobBaseDirectory = hostEnvironment.ContentRootPath;
        }

        public FileStream GetFileStream(string fileName)
        {
            return GetFileStream(Get(p => p.FileName == fileName));
        }

        public FileStream GetFileStream(Guid attachmentId)
        {
            return GetFileStream(GetById(attachmentId));
        }

        public FileStream GetFileStream(Attachment attachment)
        {
            if (attachment != null)
            {
                string targetServerURL;
                GetTargetPath(attachment.ModuleId, out targetServerURL);
                var file = $"{targetServerURL}{attachment.FilePath}";
                return File.Exists(file) ? new FileStream(file, FileMode.Open) : null;
            }
            else
                return null;
        }

        public byte[] GetFileBytes(string fileName)
        {
            return GetFileBytes(Get(p => p.FileName == fileName));
        }

        public byte[] GetFileBytes(Guid attachmentId)
        {
            return GetFileBytes(GetById(attachmentId));
        }

        public byte[] GetFileBytes(Attachment attachment)
        {
            if (attachment != null)
            {
                string targetServerURL;
                GetTargetPath(attachment.ModuleId, out targetServerURL);
                var filePath = $"{targetServerURL}{attachment.FilePath}";
                return File.Exists(filePath) ? File.ReadAllBytes(filePath) : null;
            }
            else
                return null;
        }

        public string GetFileBase64(Attachment attachment)
        {
            if (attachment != null)
            {
                string targetServerURL;
                GetTargetPath(attachment.ModuleId, out targetServerURL);
                var filePath = $"{targetServerURL}{attachment.FilePath}";
                return File.Exists(filePath) ? Convert.ToBase64String(File.ReadAllBytes(filePath)) : string.Empty;
            }
            else
                return null;
        }

        public string GetFilePath(string fileName)
        {
            return GetFilePath(Get(p => p.FileName == fileName));
        }

        public string GetFilePath(Guid attachmentId)
        {
            return GetFilePath(GetById(attachmentId));
        }

        public string GetFilePath(Attachment attachment)
        {
            if (attachment != null)
            {
                string targetServerURL;
                GetTargetPath(attachment.ModuleId, out targetServerURL);
                return $"{targetServerURL}{attachment.FilePath}";
            }
            else
                return null;
        }

        public string GetFileExtension(string fileName) => Path.GetExtension(fileName);

        public string GetFileExtension(Guid attachmentId)
        {
            var attachment = GetById(attachmentId);
            if (attachment != null)
            {
                return Path.GetExtension(attachment.FileName);
            }
            else
                return null;
        }

        public AttachmentDto GetAttachment(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;
            return GetAttachment(Get(p => p.FileName == fileName));
        }

        public AttachmentDto GetAttachment(Guid attachmentId)
        {
            return GetAttachment(GetById(attachmentId));
        }

        public AttachmentDto GetAttachment(Attachment attachment)
        {
            if (attachment != null)
            {
                var filePath = GetFilePath(attachment);
                return new AttachmentDto()
                {
                    AttachmentId = attachment.AttachmentId,
                    FilePath = filePath,
                    FileName = attachment.FileName,
                    ModuleId = attachment.ModuleId,
                    SizeByByte = attachment.SizeByByte,
                    FileData = GetFileBytes(attachment),
                    Extention = GetFileExtension(attachment.FileName),
                };
            }
            else
                return null;
        }

        public List<AttachmentDto> GetAttachment(List<Guid> attachmentsIds)
        {
            if (attachmentsIds != null && attachmentsIds.Count() != 0)
            {
                var queryData = attachmentsIds.Select(x => new
                {
                    attachmentId = x
                }).ToList();

                var queryableAttachmentId = _dataBaseContext.FromLocalList(queryData);
                var attachments = (from hpv in GetMany()
                                   join s in queryableAttachmentId on
                                    hpv.AttachmentId equals s.attachmentId
                                   select hpv).ToList();

                List<AttachmentDto> attachmentDtos = new();
                foreach (var item in attachments)
                {
                    attachmentDtos.Add(GetAttachment(item));
                }
                return attachmentDtos;
            }
            return null;
        }

        private void GetTargetPath(int moduleId, out string targetServerURL)
        {
            switch ((ModuleEnum)moduleId)
            {
                case ModuleEnum.User:
                    targetServerURL = userAttachmentServerURL;
                    break;
                default:
                    targetServerURL = null;
                    break;
            }

            if (string.IsNullOrEmpty(targetServerURL) || string.IsNullOrWhiteSpace(targetServerURL))
                targetServerURL = _blobBaseDirectory;
        }

        public List<AttachmentDto> GetAttachmentList(List<Guid> attachmentId)
        {
            if (attachmentId != null)
            {
                List<AttachmentDto> AttachmentLst = new List<AttachmentDto>();

                foreach (var item in attachmentId)
                {
                    var Attachment = new AttachmentDto();

                    Attachment = GetAttachment(item);
                    AttachmentLst.Add(Attachment);
                }
                return AttachmentLst;
            }
            else
                return null;
        }
    }
}
