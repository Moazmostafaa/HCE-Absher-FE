using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Common.Enum;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Enums;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
using HCE.Utility.Exceptions;
using HCE.Utility.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace HCE.Application.Features.LookupFeature.ClusterFeature.Commands
{
    public class DeleteClusterCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteClusterCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<Cluster> _read;
            private readonly IWriteRepository<Cluster> _write;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Cluster> read, IWriteRepository<Cluster> write, IUnitOfWork unitOfWork
               )
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;

            }
            public async Task<ResponseResult<bool>> Handle(DeleteClusterCommand request, CancellationToken cancellationToken)
            {
                var cluster = await _read.GetAsync(x => x.Id == request.Id);
                if (cluster == null)
                    throw new EntityNotFoundException(Message_Resource.ClusterEntity);

                cluster.IsDeleted = true;
                cluster.DeletedDate = DateTime.Now.GetCurrentDateTime();
                cluster.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(cluster);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteClusterCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}