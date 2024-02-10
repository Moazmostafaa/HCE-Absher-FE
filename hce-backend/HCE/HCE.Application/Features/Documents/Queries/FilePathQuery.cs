using HCE.Domain.ResponseModel;
using HCE.Interfaces.Repositories;
using HCE.Persistence.Repositories.Blob;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.Documents.Queries
{
    public class FilePathQuery : IRequest<ResponseResult<string>>
    {
        public string FileName { get; set; }
        public class Handler : IRequestHandler<FilePathQuery, ResponseResult<string>>
        {
            private readonly IReadBlobRepository _blobRepository;

            public Handler(IReadBlobRepository blobRepository)
            {
                _blobRepository = blobRepository;
            }

            public Task<ResponseResult<string>> Handle(FilePathQuery request, CancellationToken cancellationToken)
                => Task.FromResult(new ResponseResult<string>(_blobRepository.GetFilePath(request.FileName)));
        }
    }
}
