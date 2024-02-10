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

namespace HCE.Application.Features.LookupFeature.ExternalOperatorFeature.Commands
{
    public class DeleteExternalOperatorComman: CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
    private class Handler : IRequestHandler<DeleteExternalOperatorComman, ResponseResult<bool>>
    {
        private readonly IWriteRepository<ExternalOperator> _write;
        private readonly IReadRepository<ExternalOperator> _read;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserResolverHandler _userResolverHandler;

        public Handler(IUserResolverHandler userResolverHandler, IReadRepository<ExternalOperator> read, IWriteRepository<ExternalOperator> write, IUnitOfWork unitOfWork
           )
        {
            _userResolverHandler = userResolverHandler;

            _read = read;
            _write = write;
            _unitOfWork = unitOfWork;

        }
        public async Task<ResponseResult<bool>> Handle(DeleteExternalOperatorComman request, CancellationToken cancellationToken)
        {
            var externalOperator = await _read.GetAsync(x => x.Id == request.Id);
            if (externalOperator == null)
                throw new EntityNotFoundException(Message_Resource.NotFound);

                externalOperator.IsDeleted = true;
                externalOperator.DeletedDate = DateTime.Now.GetCurrentDateTime();
                externalOperator.UpdatedBy = _userResolverHandler.GetUserId();
            _write.Update(externalOperator);

            bool result = (await _unitOfWork.CommitAsync()) > 0;

            if (result)
                return new ResponseResult<bool>(true);
            else
                throw new SaveFailureException(Message_Resource.SaveField);
        }


        public class Validator : AbstractValidator<DeleteExternalOperatorComman>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }
    }
}
}