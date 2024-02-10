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
namespace HCE.Application.Features.LookupFeature.CountryFeature.Queries
{
    public class GetAllCountriesByWorldRegionIdQuery : QueryBase<ResponseResult<List<CountryDto>>>
    {
        public Guid? WorldRegionId { get; set; }

        private class Handler : IRequestHandler<GetAllCountriesByWorldRegionIdQuery, ResponseResult<List<CountryDto>>>
        {
            private readonly IReadRepository<Country> _repo;
            public Handler(IReadRepository<Country> repo)
            {
                _repo = repo;
            }

            public async Task<ResponseResult<List<CountryDto>>> Handle(GetAllCountriesByWorldRegionIdQuery request, CancellationToken cancellationToken)
            {
                if (request.WorldRegionId != null)
                {
                    var query = _repo.GetManyAsNoTracking(x => x.WordRegionId == request.WorldRegionId);

                    var data = await query.Select(x => new CountryDto()
                    {
                        CountryId = x.Id,
                        CountryNameAr = x.CountryNameAr,
                        CountryNameEn = x.CountryNameEn,
                        CountryNameLang = x.CountryNameLang,
                        CountryDesc = x.CountryDesc,
                        WorldRegionId = x.WordRegionId,
                        WorldRegionNameAr = x.WorldRegion.WorldRegionNameAr,
                        WorldRegionNameEn = x.WorldRegion.WorldRegionNameEn,
                        WorldRegionNameLang = x.WorldRegion.WorldRegionNameLang,


                        CreatedBy = x.UserId,
                        CreationDate = x.CreatedDate

                    }).ToListAsync(cancellationToken);

                    return new ResponseResult<List<CountryDto>>(data);
                }
                else
                {
                    var query = _repo.GetManyAsNoTracking();

                    var data = await query.Select(x => new CountryDto()
                    {
                        CountryId = x.Id,
                        CountryNameAr = x.CountryNameAr,
                        CountryNameEn = x.CountryNameEn,
                        CountryNameLang = x.CountryNameLang,
                        CountryDesc = x.CountryDesc,
                        WorldRegionId = x.WordRegionId,
                        WorldRegionNameAr = x.WorldRegion.WorldRegionNameAr,
                        WorldRegionNameEn = x.WorldRegion.WorldRegionNameEn,
                        WorldRegionNameLang = x.WorldRegion.WorldRegionNameLang,


                        CreatedBy = x.UserId,
                        CreationDate = x.CreatedDate

                    }).ToListAsync(cancellationToken);

                    return new ResponseResult<List<CountryDto>>(data);
                }
            }
        }
    }
}
