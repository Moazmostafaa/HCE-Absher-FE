using HCE.Application.Common.Validators;
using HCE.Application.Features.Documents.Queries;
using HCE.Resource;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Application.Features.Documents.Validators
{
    public class DownloadFileQueryValidator : AbstractValidator<DownloadFileByNameQuery>
    {
        public DownloadFileQueryValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage(Message_Resource.FileExtensionNotValid);
            RuleFor(x => x.FileName).Must(name => FileValidator._extensions.Contains(Path.GetExtension(name).ToLower()))
                .WithMessage(string.Format(Message_Resource.FileExtensionNotValid, string.Join(", ", FileValidator._extensions)));
        }
    }
}
