using FluentValidation;
using HCE.Application.Common.Validators;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Domain.Request;
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

namespace HCE.Application.Features.LookupFeature.CityFeature.Queries
{
    public class GetAllCitiesByStateRegionIdQuery : IRequest<ResponseResult<PagedResponseResult<CityDto>>>, IPagedRequest
    {
        public Guid StateRegionId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllCitiesByStateRegionIdQuery, ResponseResult<PagedResponseResult<CityDto>>>
        {

            private readonly IReadRepository<City> _countryReadRepository;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<City> CountryReadRepository, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _countryReadRepository = CountryReadRepository;
            }
            public async Task<ResponseResult<PagedResponseResult<CityDto>>> Handle(GetAllCitiesByStateRegionIdQuery request, CancellationToken cancellationToken)
            {
                var query = _countryReadRepository.GetManyAsNoTracking(x => x.StateRegionId==request.StateRegionId,
                                                  include: x => x.Include(c => c.StateRegion));

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<CityDto>>
                {
                    Entity = new PagedResponseResult<CityDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new CityDto()
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




                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllCitiesByStateRegionIdQuery>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                    RuleFor(x => x.StateRegionId).NotEmpty();

                }
            }
        }
    }
}