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

namespace HCE.Application.Features.LookupFeature.OperatorGroupFeature.Queries
{
    public class GetAllOperatorGroupsQuery : IRequest<ResponseResult<PagedResponseResult<OperatorGroupDto>>>, IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllOperatorGroupsQuery, ResponseResult<PagedResponseResult<OperatorGroupDto>>>
        {

            private readonly IReadRepository<OperatorGroup> _read;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<OperatorGroup> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }
            public async Task<ResponseResult<PagedResponseResult<OperatorGroupDto>>> Handle(GetAllOperatorGroupsQuery request, CancellationToken cancellationToken)
            {
                var query = _read.GetManyAsNoTracking();

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<OperatorGroupDto>>
                {
                    Entity = new PagedResponseResult<OperatorGroupDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new OperatorGroupDto()
                        {
                            OperatorGroupId = x.Id,
                            OperatorGroupName = x.OperatorGroupName,
                            OperatorGroupDesc = x.OperatorGroupDesc,
                            CreatedBy = x.UserId,
                            CreationDate = x.CreatedDate

                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllOperatorGroupsQuery>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}
