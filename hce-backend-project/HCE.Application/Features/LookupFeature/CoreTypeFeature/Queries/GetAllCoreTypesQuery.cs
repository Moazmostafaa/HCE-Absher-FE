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


namespace HCE.Application.Features.LookupFeature.CoreTypeFeature.Queries
{
    public class GetAllCoreTypesQuery : IRequest<ResponseResult<PagedResponseResult<CoreTypeDto>>>, IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllCoreTypesQuery, ResponseResult<PagedResponseResult<CoreTypeDto>>>
        {

            private readonly IReadRepository<CoreType> _ReadRepository;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<CoreType> ReadRepository, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _ReadRepository = ReadRepository;
            }
            public async Task<ResponseResult<PagedResponseResult<CoreTypeDto>>> Handle(GetAllCoreTypesQuery request, CancellationToken cancellationToken)
            {
                var query = _ReadRepository.GetManyAsNoTracking();

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<CoreTypeDto>>
                {
                    Entity = new PagedResponseResult<CoreTypeDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new CoreTypeDto()
                        {
                             NPSKPIWeightId= x.Id,
                            NPSKPIWeightName = x.NPSKPIWeightName,
                            NPSKPIWeightDesc = x.NPSKPIWeightDesc,

                            CreatedBy = x.UserId,
                            CreationDate = x.CreatedDate

                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllCoreTypesQuery>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}


