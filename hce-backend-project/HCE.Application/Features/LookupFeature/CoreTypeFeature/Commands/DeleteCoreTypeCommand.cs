using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
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
namespace HCE.Application.Features.LookupFeature.CoreTypeFeature.Commands
{
    public class DeleteCoreTypeCommand : CommandBase<ResponseResult<bool>>
    {

        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteCoreTypeCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<CoreType> _read;
            private readonly IWriteRepository<CoreType> _write;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<CoreType> read, IWriteRepository<CoreType> write, IUnitOfWork unitOfWork
               )
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;

            }
            public async Task<ResponseResult<bool>> Handle(DeleteCoreTypeCommand request, CancellationToken cancellationToken)
            {
                var coreType = await _read.GetAsync(x => x.Id == request.Id);
                if (coreType == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                coreType.IsDeleted = true;
                coreType.DeletedDate = DateTime.Now.GetCurrentDateTime();
                coreType.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(coreType);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteCoreTypeCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}
