using FluentValidation;
using HCE.Application.Common;
using HCE.Application.Common.Validators;
using HCE.Domain.Entities.Common.Enum;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Domain.Request;
using HCE.Interfaces.Enums;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.LookupFeature.WorldRegionFeature.Queries
{
    public class GetAllWorldRegionsQuery : IRequest<ResponseResult<PagedResponseResult<WorldRegionDto>>>, IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllWorldRegionsQuery, ResponseResult<PagedResponseResult<WorldRegionDto>>>
        {

            private readonly IReadRepository<WorldRegion> _regionReadRepository;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<WorldRegion> RegionReadRepository, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _regionReadRepository = RegionReadRepository;
            }
            public async Task<ResponseResult<PagedResponseResult<WorldRegionDto>>> Handle(GetAllWorldRegionsQuery request, CancellationToken cancellationToken)
            {
                var query = _regionReadRepository.GetManyAsNoTracking();
                                                  

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<WorldRegionDto>>
                {
                    Entity = new PagedResponseResult<WorldRegionDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new WorldRegionDto()
                        {
                            RegionId = x.Id,
                            RegionNameAr = x.WorldRegionNameAr,
                            RegionNameEn = x.WorldRegionNameEn,
                            RegionNameLang = x.WorldRegionNameLang,
                            RegionDesc = x.WorldRegionDesc,
       

                            CreatedBy = x.UserId,
                            CreationDate = x.CreatedDate



                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllWorldRegionsQuery>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}
