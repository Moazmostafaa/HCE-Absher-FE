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


namespace HCE.Application.Features.LookupFeature.CityFeature.Queries
{
    public class GetAllCitiesByStateRegionWithoutPag : QueryBase<ResponseResult<List<CityDto>>>
    {
        public Guid? StateRegionId { get; set; }

        private class Handler : IRequestHandler<GetAllCitiesByStateRegionWithoutPag, ResponseResult<List<CityDto>>>
        {
            private readonly IReadRepository<City> _repo;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<City> repo, IUserResolverHandler userResolverHandler)
            {
                _repo = repo;
                _userResolverHandler = userResolverHandler;
            }

            public async Task<ResponseResult<List<CityDto>>> Handle(GetAllCitiesByStateRegionWithoutPag request, CancellationToken cancellationToken)
            {
                if (request.StateRegionId != null)
                {
                    var query = _repo.GetManyAsNoTracking(x => x.StateRegionId == request.StateRegionId);

                    var data = await query.Select(x => new CityDto()
                    {
                        CityId = x.Id,
                        CityNameAr = x.CityNameAr,
                        CityNameEn = x.CityNameEn,
                        CityNameLang = x.CityNameLang,
                        CityDesc = x.CityDesc,
                        CreatedBy = _userResolverHandler.GetUserGuid(),
                        CreationDate = x.CreatedDate,
                        StateRegionId = x.StateRegionId,
                        IsTop = x.IsTop,
                        StateRegionNameAr = x.StateRegion.StateRegionNameAr,
                        StateRegionNameEn = x.StateRegion.StateRegionNameEn,
                        StateRegionNameLang = x.StateRegion.StateRegionNameLang

                    }).ToListAsync(cancellationToken);

                    return new ResponseResult<List<CityDto>>(data);
                }
                else
                {
                    var query = _repo.GetManyAsNoTracking();

                    var data = await query.Select(x => new CityDto()
                    {
                        CityId = x.Id,
                        CityNameAr = x.CityNameAr,
                        CityNameEn = x.CityNameEn,
                        CityNameLang = x.CityNameLang,
                        CityDesc = x.CityDesc,
                        CreatedBy = _userResolverHandler.GetUserGuid(),
                        CreationDate = x.CreatedDate,
                        StateRegionId = x.StateRegionId,
                        IsTop = x.IsTop,
                        StateRegionNameAr = x.StateRegion.StateRegionNameAr,
                        StateRegionNameEn = x.StateRegion.StateRegionNameEn,
                        StateRegionNameLang = x.StateRegion.StateRegionNameLang

                    }).ToListAsync(cancellationToken);

                    return new ResponseResult<List<CityDto>>(data);
                }
            }
        }
    }
}