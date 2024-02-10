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
    public class DownloadFileByNameQuery : IRequest<FileStream>
    {
        public string FileName { get; set; }
        public class Handler : IRequestHandler<DownloadFileByNameQuery, FileStream>
        {
            private readonly IReadBlobRepository _blobRepository;

            public Handler(IReadBlobRepository blobRepository)
            {
                _blobRepository = blobRepository;
            }

            public Task<FileStream> Handle(DownloadFileByNameQuery request, CancellationToken cancellationToken)
            {
                var stream = _blobRepository.GetFileStream(request.FileName);
                if (stream is null)
                    throw new FileNotFoundException();

                return Task.FromResult(stream);
            }
        }
    }

}
