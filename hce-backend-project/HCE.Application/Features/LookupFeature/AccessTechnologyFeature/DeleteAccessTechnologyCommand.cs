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

namespace HCE.Application.Features.LookupFeature.AccessTechnologyFeature
{
    public class DeleteAccessTechnologyCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteAccessTechnologyCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<AccessTechnology> _read;
            private readonly IWriteRepository<AccessTechnology> _write;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<AccessTechnology> read, IWriteRepository<AccessTechnology> write, IUnitOfWork unitOfWork
               )
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;

            }
            public async Task<ResponseResult<bool>> Handle(DeleteAccessTechnologyCommand request, CancellationToken cancellationToken)
            {
                var accessTechnology = await _read.GetAsync(x => x.Id == request.Id);
                if (accessTechnology == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                accessTechnology.IsDeleted = true;
                accessTechnology.DeletedDate = DateTime.Now.GetCurrentDateTime();
                accessTechnology.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(accessTechnology);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteAccessTechnologyCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}
