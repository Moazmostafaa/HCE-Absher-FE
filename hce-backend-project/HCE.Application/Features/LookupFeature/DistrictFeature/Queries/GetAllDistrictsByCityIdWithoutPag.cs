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

namespace HCE.Application.Features.LookupFeature.DistrictFeature.Queries
{
    public class GetAllDistrictsByCityIdWithoutPag : QueryBase<ResponseResult<List<DistrictDto>>>
    {
        public Guid? CityId { get; set; }

        private class Handler : IRequestHandler<GetAllDistrictsByCityIdWithoutPag, ResponseResult<List<DistrictDto>>>
        {
            private readonly IReadRepository<District> _repo;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<District> repo, IUserResolverHandler userResolverHandler)
            {
                _repo = repo;
                _userResolverHandler = userResolverHandler;
            }

            public async Task<ResponseResult<List<DistrictDto>>> Handle(GetAllDistrictsByCityIdWithoutPag request, CancellationToken cancellationToken)
            {
                if (request.CityId != null)
                {
                    var query = _repo.GetManyAsNoTracking(x => x.CityId == request.CityId);

                    var data = await query.Select(x => new DistrictDto()
                    {

                        DistrictId = x.Id,
                        DistrictNameAr = x.DistrictNameAr,
                        DistrictNameEn = x.DistrictNameEn,
                        DistrictNameLang = x.DistrictNameLang,
                        DistrictDesc = x.DistrictDesc,
                        CityId = x.City.Id,
                        CityNameAr = x.City.CityNameAr,
                        CityNameEn = x.City.CityNameEn,
                        CityNameLang = x.City.CityNameLang,


                        CreationDate = x.CreatedDate,
                        CreatedBy = x.UserId

                    }).ToListAsync(cancellationToken);

                    return new ResponseResult<List<DistrictDto>>(data);
                }
                else
                {
                    var query = _repo.GetManyAsNoTracking();

                    var data = await query.Select(x => new DistrictDto()
                    {

                        DistrictId = x.Id,
                        DistrictNameAr = x.DistrictNameAr,
                        DistrictNameEn = x.DistrictNameEn,
                        DistrictNameLang = x.DistrictNameLang,
                        DistrictDesc = x.DistrictDesc,
                        CityId = x.City.Id,
                        CityNameAr = x.City.CityNameAr,
                        CityNameEn = x.City.CityNameEn,
                        CityNameLang = x.City.CityNameLang,


                        CreationDate = x.CreatedDate,
                        CreatedBy = x.UserId

                    }).ToListAsync(cancellationToken);

                    return new ResponseResult<List<DistrictDto>>(data);
                }
            }
        }
    }
}