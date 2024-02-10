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
    public class UpdateGoalCommand : CommandBase<ResponseResult<GoalDto>>
    {
        public Guid GoalId { get; set; }

        public string GoalName { get; set; }

        public string GoalDesc { get; set; }



        private class Handler : IRequestHandler<UpdateGoalCommand, ResponseResult<GoalDto>>
        {
            private readonly IWriteRepository<Goal> _write;
            private readonly IReadRepository<Goal> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Goal> read, IWriteRepository<Goal> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<GoalDto>> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
            {
                var goal = await _read.GetAsync(x => x.Id == request.GoalId);
                if (goal == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                goal.GoalName = request.GoalName;
                goal.GoalDesc = request.GoalDesc;
                goal.UpdatedBy = _userResolverHandler.GetUserId();
                goal.UpdatedDate= DateTime.Now.GetCurrentDateTime();

                 _write.Update(goal);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<GoalDto>()
                {

                    Entity = new GoalDto()
                    {
                        GoalId = goal.Id,
                        GoalName = goal.GoalName,
                        GoalDesc = goal.GoalDesc,
                        CreationDate = goal.CreatedDate,
                        CreatedBy = goal.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateGoalCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.GoalId).NotEmpty();

                    RuleFor(x => x.GoalName).NotEmpty();


                }
            }

        }
    }
}

