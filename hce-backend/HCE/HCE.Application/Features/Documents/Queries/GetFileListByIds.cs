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
   public class GetFileListByIds : QueryBase<ResponseResult<List<AttachmentDto>>>
    {
        public List<Guid> AttachmentId { get; set; }
        public class Handler : IRequestHandler<GetFileListByIds, ResponseResult<List<AttachmentDto>>>
        {
            private readonly IReadBlobRepository _blobRepository;

            public Handler(IReadBlobRepository blobRepository)
            {
                _blobRepository = blobRepository;
            }

            public Task<ResponseResult<List<AttachmentDto>>> Handle(GetFileListByIds request, CancellationToken cancellationToken)
            {
                List<AttachmentDto> AttachmentLst = new List<AttachmentDto>();
                 AttachmentLst = _blobRepository.GetAttachment(request.AttachmentId);

                if (AttachmentLst is null)
                    throw new FileNotFoundException();

                var result = new ResponseResult<List<AttachmentDto>>
                {
                    IsSuccess = true,
                    Entity = AttachmentLst,
                    Status = HttpStatusCode.OK
                };
                return Task.FromResult(result);
            }
        }
    }

}
