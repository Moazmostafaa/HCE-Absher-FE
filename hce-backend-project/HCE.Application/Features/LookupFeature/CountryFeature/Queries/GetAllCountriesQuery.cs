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
    public class GetAllCountriesQuery : IRequest<ResponseResult<PagedResponseResult<CountryDto>>>, IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllCountriesQuery, ResponseResult<PagedResponseResult<CountryDto>>>
        {

            private readonly IReadRepository<Country> _countryReadRepository;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Country> CountryReadRepository, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _countryReadRepository = CountryReadRepository;
            }
            public async Task<ResponseResult<PagedResponseResult<CountryDto>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
            {
                var query = _countryReadRepository.GetManyAsNoTracking(x => x.IsDeleted == false,
                                                  include: x => x.Include(c => c.WorldRegion));

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<CountryDto>>
                {
                    Entity = new PagedResponseResult<CountryDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new CountryDto()
                        {
                            CountryId = x.Id,
                            CountryNameAr = x.CountryNameAr,
                            CountryNameEn = x.CountryNameEn,
                            CountryNameLang = x.CountryNameLang,
                          CountryDesc=x.CountryDesc,
                          WorldRegionId=x.WordRegionId,
                          WorldRegionNameAr=x.WorldRegion.WorldRegionNameAr,
                          WorldRegionNameEn=x.WorldRegion.WorldRegionNameEn,
                         WorldRegionNameLang=x.WorldRegion.WorldRegionNameLang,
                         

                            CreatedBy = x.UserId,
                            CreationDate = x.CreatedDate



                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllCountriesQuery>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}

