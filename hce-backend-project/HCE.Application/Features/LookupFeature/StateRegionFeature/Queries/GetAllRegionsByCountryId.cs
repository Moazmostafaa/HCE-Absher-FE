﻿using FluentValidation;
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

namespace HCE.Application.Features.LookupFeature.RegionFeature.Queries
{
   public  class GetAllRegionsByCountryId : IRequest<ResponseResult<PagedResponseResult<StateRegionDto>>>, IPagedRequest
    {
        public Guid CountryId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllRegionsByCountryId, ResponseResult<PagedResponseResult<StateRegionDto>>>
        {

            private readonly IReadRepository<StateRegion> _regionReadRepository;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<StateRegion> RegionReadRepository, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _regionReadRepository = RegionReadRepository;
            }
            public async Task<ResponseResult<PagedResponseResult<StateRegionDto>>> Handle(GetAllRegionsByCountryId request, CancellationToken cancellationToken)
            {
                var query = _regionReadRepository.GetManyAsNoTracking(x => x.CountryId==request.CountryId,
                                                  include: x => x.Include(c => c.Country));

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<StateRegionDto>>
                {
                    Entity = new PagedResponseResult<StateRegionDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new StateRegionDto()
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



                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllRegionsByCountryId>
            {
                public Validator()
                {
                    RuleFor(x => x.CountryId).NotEmpty();

                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}
