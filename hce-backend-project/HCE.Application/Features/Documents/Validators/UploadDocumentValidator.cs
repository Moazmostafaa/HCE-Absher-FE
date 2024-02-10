using HCE.Application.Common.Validators;
using HCE.Application.Features.Documents.Commands;
using HCE.Utility.CommonModels;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Application.Features.Documents.Validators
{
    public class UploadDocumentValidator : AbstractValidator<UploadDocumentCommand>
    {
        public UploadDocumentValidator(IOptions<AppSetting> options)
        {
            RuleFor(x => x.File).SetValidator(new FileValidator(options));
        }
    }
}
