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

namespace HCE.Application.Features.LookupFeature.OperatorFeature.Queries
{
    public class GetOperatorsQuery : IRequest<ResponseResult<PagedResponseResult<OperatorDto>>>, IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetOperatorsQuery, ResponseResult<PagedResponseResult<OperatorDto>>>
        {

            private readonly IReadRepository<Operator> _read;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Operator> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }
            public async Task<ResponseResult<PagedResponseResult<OperatorDto>>> Handle(GetOperatorsQuery request, CancellationToken cancellationToken)
            {
                var query = _read.GetManyAsNoTracking(x=>x.IsDeleted==false,include: x => x.Include(c => c.OperatorGroup)); ;

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
                var result = new ResponseResult<PagedResponseResult<OperatorDto>>
                {
                    Entity = new PagedResponseResult<OperatorDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new OperatorDto()
                        {
                            OperatorId = x.Id,
                            OperatorName = x.OperatorName,
                            OperatorDesc = x.OperatorDesc,
                            CreatedBy = x.UserId,
                            CreationDate = x.CreatedDate,
                            OperatorGroupId=x.OperatorGroup.Id,
                            OperatorGroupName=x.OperatorGroup.OperatorGroupName

                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetOperatorsQuery>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}