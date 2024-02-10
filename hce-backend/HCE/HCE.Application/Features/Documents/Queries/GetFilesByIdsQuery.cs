using HCE.Application.Common;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.User.UserAttachment;
using HCE.Interfaces.Repositories;
using HCE.Persistence.Repositories.Blob;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.Documents.Queries
{
    public class GetFilesByIdsQuery : QueryBase<ResponseResult<List<AttachmentDto>>>
    {
        public List<Guid> AttachmentsIds { get; set; }
        public class Handler : IRequestHandler<GetFilesByIdsQuery, ResponseResult<List<AttachmentDto>>>
        {
            private readonly IReadBlobRepository _blobRepository;

            public Handler(IReadBlobRepository blobRepository)
            {
                _blobRepository = blobRepository;
            }

            public Task<ResponseResult<List<AttachmentDto>>> Handle(GetFilesByIdsQuery request, CancellationToken cancellationToken)
            {
                var attachments = _blobRepository.GetAttachment(request.AttachmentsIds);
                if (attachments is null)
                    throw new FileNotFoundException();

                var result = new ResponseResult<List<AttachmentDto>>
                {
                    IsSuccess = true,
                    Entity = attachments,
                    Status = HttpStatusCode.OK
                };
                return Task.FromResult(result);
            }
        }
    }

}
