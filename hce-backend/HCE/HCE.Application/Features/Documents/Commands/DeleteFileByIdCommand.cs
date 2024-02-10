using HCE.Persistence.Repositories.Blob;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.Documents.Commands
{
    public class DeleteFileByIdCommand : IRequest
    {
        public Guid AttachmentId { get; set; }

        public class Handler : IRequestHandler<DeleteFileByIdCommand>
        {
            private readonly IWriteBlobRepository _blobRepository;

            public Handler(IWriteBlobRepository blobRepository)
            {
                _blobRepository = blobRepository;
            }

            public Task<Unit> Handle(DeleteFileByIdCommand request, CancellationToken cancellationToken)
                => _blobRepository.Delete(request.AttachmentId).ContinueWith(_ => Unit.Value, cancellationToken);
        }
    }
}
