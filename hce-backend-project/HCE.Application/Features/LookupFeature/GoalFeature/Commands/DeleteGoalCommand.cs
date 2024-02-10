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
namespace HCE.Application.Features.LookupFeature.GoalFeature.Commands
{
    public class DeleteGoalCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteGoalCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<Goal> _read;
            private readonly IWriteRepository<Goal> _write;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Goal> read, IWriteRepository<Goal> write, IUnitOfWork unitOfWork
               )
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;

            }
            public async Task<ResponseResult<bool>> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
            {
                var goal = await _read.GetAsync(x => x.Id == request.Id);
                if (goal == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                goal.IsDeleted = true;
                goal.DeletedDate = DateTime.Now.GetCurrentDateTime();
                goal.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(goal);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteGoalCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}
