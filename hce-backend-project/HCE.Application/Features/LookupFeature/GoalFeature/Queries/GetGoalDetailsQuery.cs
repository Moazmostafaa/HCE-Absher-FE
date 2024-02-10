using FluentValidation;
using HCE.Application.Common;
using HCE.Application.Common.Validators;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Domain.Request;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
using HCE.Utility.Exceptions;
using HCE.Utility.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.LookupFeature.GoalFeature.Queries
{
    public class GetGoalDetailsQuery : IRequest<ResponseResult<GoalDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetGoalDetailsQuery, ResponseResult<GoalDto>>
        {
            private readonly IReadRepository<Goal> _regionReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Goal> regionReadRepositor, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _regionReadRepository = regionReadRepositor;
            }

            public async Task<ResponseResult<GoalDto>> Handle(GetGoalDetailsQuery request, CancellationToken cancellationToken)
            {
                var goal = await _regionReadRepository.GetAsync(x => x.Id == request.Id
                                                   );
                if (goal == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<GoalDto>
                {
                    Entity = new GoalDto()
                    {
                        GoalId = goal.Id,
                        GoalName = goal.GoalName,
                        GoalDesc = goal.GoalDesc,
                     
                        CreationDate = goal.CreatedDate,
                        CreatedBy = goal.UserId,
               
                        
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetGoalDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}