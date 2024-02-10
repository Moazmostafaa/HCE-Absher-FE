using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
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
    public class AddGoalCommand : CommandBase<ResponseResult<GoalDto>>
    {
        public string GoalName { get; set; }
  
        public string GoalDesc { get; set; }
  


        private class Handler : IRequestHandler<AddGoalCommand, ResponseResult<GoalDto>>
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

            public async Task<ResponseResult<GoalDto>> Handle(AddGoalCommand request, CancellationToken cancellationToken)
            {
                var goal = new Goal
                {
                    GoalName = request.GoalName,
                    GoalDesc = request.GoalDesc,
                    UserId = _userResolverHandler.GetUserGuid(),
               
                };

                await _write.AddAsync(goal);

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


            public class Validator : AbstractValidator<AddGoalCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.GoalName).NotEmpty();


                }
            }

        }
    }
}

