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
    public class GetAllWorldRegionsWithoutPaginationQuery : QueryBase<ResponseResult<List<WorldRegionDto>>>
    {

        private class Handler : IRequestHandler<GetAllWorldRegionsWithoutPaginationQuery, ResponseResult<List<WorldRegionDto>>>
        {
            private readonly IReadRepository<WorldRegion> _repo;
            public Handler(IReadRepository<WorldRegion> repo )
            {
                _repo = repo;
            }

            public async Task<ResponseResult<List<WorldRegionDto>>> Handle(GetAllWorldRegionsWithoutPaginationQuery request, CancellationToken cancellationToken)
            {
                var query = _repo.GetManyAsNoTracking();

                var data = await query.Select(x => new WorldRegionDto()
                {
                    RegionId = x.Id,
                    RegionNameAr = x.WorldRegionNameAr,
                    RegionNameEn = x.WorldRegionNameEn,
                    RegionNameLang = x.WorldRegionNameLang,
                    RegionDesc = x.WorldRegionDesc,


                    CreatedBy = x.UserId,
                    CreationDate = x.CreatedDate

                }).ToListAsync(cancellationToken);

                return new ResponseResult<List<WorldRegionDto>>(data);
            }
        }
    }
}
