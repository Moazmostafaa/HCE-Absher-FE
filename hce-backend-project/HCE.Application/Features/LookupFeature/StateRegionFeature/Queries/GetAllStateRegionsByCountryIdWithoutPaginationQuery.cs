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

namespace HCE.Application.Features.LookupFeature.StateRegionFeature.Queries
{
    public class GetAllStateRegionsByCountryIdWithoutPaginationQuery : QueryBase<ResponseResult<List<StateRegionDto>>>
    {
        public Guid? CountryId { get; set; }

        private class Handler : IRequestHandler<GetAllStateRegionsByCountryIdWithoutPaginationQuery, ResponseResult<List<StateRegionDto>>>
        {
            private readonly IReadRepository<StateRegion> _repo;
            public Handler(IReadRepository<StateRegion> repo)
            {
                _repo = repo;
            }

            public async Task<ResponseResult<List<StateRegionDto>>> Handle(GetAllStateRegionsByCountryIdWithoutPaginationQuery request, CancellationToken cancellationToken)
            {
                if (request.CountryId != null)
                {
                    var query = _repo.GetManyAsNoTracking(x => x.CountryId == request.CountryId);

                    var data = await query.Select(x => new StateRegionDto()
                    {
                        RegionId = x.Id,
                        RegionNameAr = x.StateRegionNameAr,
                        RegionNameEn = x.StateRegionNameEn,
                        RegionNameLang = x.StateRegionNameLang,
                        RegionDesc = x.StateRegionDesc,
                        CountryId = x.CountryId,
                        CountryNameAr = x.Country.CountryNameAr,
                        CountryNameEn = x.Country.CountryNameEn,

                        CountryNameLang = x.Country.CountryNameLang,

                        CreatedBy = x.UserId,
                        CreationDate = x.CreatedDate

                    }).ToListAsync(cancellationToken);

                    return new ResponseResult<List<StateRegionDto>>(data);
                }
                else
                {
                    var query = _repo.GetManyAsNoTracking();

                    var data = await query.Select(x => new StateRegionDto()
                    {
                        RegionId = x.Id,
                        RegionNameAr = x.StateRegionNameAr,
                        RegionNameEn = x.StateRegionNameEn,
                        RegionNameLang = x.StateRegionNameLang,
                        RegionDesc = x.StateRegionDesc,
                        CountryId = x.CountryId,
                        CountryNameAr = x.Country.CountryNameAr,
                        CountryNameEn = x.Country.CountryNameEn,

                        CountryNameLang = x.Country.CountryNameLang,

                        CreatedBy = x.UserId,
                        CreationDate = x.CreatedDate

                    }).ToListAsync(cancellationToken);

                    return new ResponseResult<List<StateRegionDto>>(data);
                }
            }
        }
    }
}
