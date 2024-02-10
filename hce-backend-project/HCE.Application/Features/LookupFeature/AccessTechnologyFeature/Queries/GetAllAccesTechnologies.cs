using FluentValidation;
using HCE.Application.Common.Validators;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Domain.Request;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.LookupFeature.AccessTechnologyFeature.Queries
{
    public class GetAllAccesTechnologies : IRequest<ResponseResult<PagedResponseResult<AccessTechnologyDto>>>, IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllAccesTechnologies, ResponseResult<PagedResponseResult<AccessTechnologyDto>>>
        {

            private readonly IReadRepository<AccessTechnology> _read;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<AccessTechnology> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }
            public async Task<ResponseResult<PagedResponseResult<AccessTechnologyDto>>> Handle(GetAllAccesTechnologies request, CancellationToken cancellationToken)
            {
                var query = _read.GetManyAsNoTracking();

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<AccessTechnologyDto>>
                {
                    Entity = new PagedResponseResult<AccessTechnologyDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new AccessTechnologyDto()
                        {
                            ServiceId = x.Id,
                            ServiceName = x.ServiceName,
                            ServiceDesc = x.ServiceDesc,

                            CreatedBy = x.UserId,
                            CreationDate = x.CreatedDate

                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllAccesTechnologies>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}


