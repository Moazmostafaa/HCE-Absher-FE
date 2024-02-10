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

namespace HCE.Application.Features.LookupFeature.PriorityFeature.Queries
{
    public class GetAllPrioritiesQuery : IRequest<ResponseResult<PagedResponseResult<PriorityDto>>>, IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllPrioritiesQuery, ResponseResult<PagedResponseResult<PriorityDto>>>
        {

            private readonly IReadRepository<Priority> _read;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Priority> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }
            public async Task<ResponseResult<PagedResponseResult<PriorityDto>>> Handle(GetAllPrioritiesQuery request, CancellationToken cancellationToken)
            {
                var query = _read.GetManyAsNoTracking();

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<PriorityDto>>
                {
                    Entity = new PagedResponseResult<PriorityDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new PriorityDto()
                        {
                            PriorityId = x.Id,
                            PriorityName = x.PriorityName,
                            PriorityDesc = x.PriorityDesc,
                            CreatedBy = x.UserId,
                            CreationDate = x.CreatedDate

                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllPrioritiesQuery>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}
