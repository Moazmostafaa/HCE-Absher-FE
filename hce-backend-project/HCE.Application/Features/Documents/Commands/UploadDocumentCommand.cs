using HCE.Domain.Entities.Common.Enum;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Repositories;
using HCE.Persistence.Repositories.Blob;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.Documents.Commands
{
    public class UploadDocumentCommand : IRequest<ResponseResult<Guid>>
    {
        public ModuleEnum ModuleId { get; set; }
        public IFormFile File { get; set; }

        public class Handler : IRequestHandler<UploadDocumentCommand, ResponseResult<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IWriteBlobRepository _blobRepository;

            public Handler(IUnitOfWork unitOfWork, IWriteBlobRepository blobRepository)
            {
                _unitOfWork = unitOfWork;
                _blobRepository = blobRepository;
            }

            public async Task<ResponseResult<Guid>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
            {
                var attachmentId = await _blobRepository.PersistAsync(request.File.OpenReadStream(), request.File.FileName, (int)request.ModuleId, cancellationToken);
                await _unitOfWork.CommitAsync();
                return new ResponseResult<Guid>(attachmentId);
            }
        }
    }
}
