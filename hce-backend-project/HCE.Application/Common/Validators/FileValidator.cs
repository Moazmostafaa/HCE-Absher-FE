using HCE.Utility.CommonModels;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;

namespace HCE.Application.Common.Validators
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public static List<string> _extensions = new()
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".mp4",
            ".mov",
            ".avi",
            ".3gp",
            ".mp3",
            ".wav",
            ".ogg",
            ".pdf",
            ".pptx",
            ".ppt",
            ".txt",
            ".doc",
            ".docx",
            ".xls",
            ".xlsx",
            ".csv",
            ".txt"
        };



        public FileValidator(IOptions<AppSetting> options)
        {
            RuleFor(file => file.Length).LessThanOrEqualTo(options.Value.MaxFileSize * 1024 * 1024).WithMessage($"حجم الملف لابد ان يكون اقل من {options.Value.MaxFileSize}");
            RuleFor(file => file.FileName).Must(name => _extensions.Contains(Path.GetExtension(name).ToLower())).WithMessage($"نوع الملف مرفوض,الانواع المتاحة هي ({string.Join(", ", _extensions)}).");
        }
    }

}
