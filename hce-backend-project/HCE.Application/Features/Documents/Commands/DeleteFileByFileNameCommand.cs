using HCE.Persistence.Repositories.Blob;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.Documents.Commands
{
    public class DeleteFileByFileNameCommand : IRequest
    {
        public string FileName { get; set; }

        public class Handler : IRequestHandler<DeleteFileByFileNameCommand>
        {
            private readonly IWriteBlobRepository _blobRepository;

            public Handler(IWriteBlobRepository blobRepository)
            {
                _blobRepository = blobRepository;
            }

            public Task<Unit> Handle(DeleteFileByFileNameCommand request, CancellationToken cancellationToken)
                => _blobRepository.Delete(request.FileName).ContinueWith(_ => Unit.Value, cancellationToken);
        }
    }
}
