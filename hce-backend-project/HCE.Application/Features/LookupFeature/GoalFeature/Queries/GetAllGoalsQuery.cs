﻿using FluentValidation;
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
    public class GetAllGoalsQuery : IRequest<ResponseResult<PagedResponseResult<GoalDto>>>, IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllGoalsQuery, ResponseResult<PagedResponseResult<GoalDto>>>
        {

            private readonly IReadRepository<Goal> _goalReadRepository;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Goal> GoalReadRepository, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _goalReadRepository = GoalReadRepository;
            }
            public async Task<ResponseResult<PagedResponseResult<GoalDto>>> Handle(GetAllGoalsQuery request, CancellationToken cancellationToken)
            {
                var query = _goalReadRepository.GetManyAsNoTracking();

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<GoalDto>>
                {
                    Entity = new PagedResponseResult<GoalDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new GoalDto()
                        {
                            GoalId = x.Id,
                            GoalName = x.GoalName,
                            GoalDesc = x.GoalDesc,

                            CreatedBy = x.UserId,
                            CreationDate = x.CreatedDate
                            
                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllGoalsQuery>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}


