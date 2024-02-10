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
    public class GetFileByIdQuery : QueryBase<ResponseResult<AttachmentDto>>
    {
        public Guid AttachmentId { get; set; }
        public class Handler : IRequestHandler<GetFileByIdQuery, ResponseResult<AttachmentDto>>
        {
            private readonly IReadBlobRepository _blobRepository;

            public Handler(IReadBlobRepository blobRepository)
            {
                _blobRepository = blobRepository;
            }

            public Task<ResponseResult<AttachmentDto>> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
            {
                var attachment = _blobRepository.GetAttachment(request.AttachmentId);
                if (attachment is null)
                    throw new FileNotFoundException();

                var result = new ResponseResult<AttachmentDto>
                {
                    IsSuccess = true,
                    Entity = attachment,
                    Status = HttpStatusCode.OK
                };
                return Task.FromResult(result);
            }
        }
    }

}
