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
    public class GetCountryDetailsQuery : IRequest<ResponseResult<CountryDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetCountryDetailsQuery, ResponseResult<CountryDto>>
        {
            private readonly IReadRepository<Country> _CountryReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Country> CountryReadRepositor, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _CountryReadRepository = CountryReadRepositor;
            }

            public async Task<ResponseResult<CountryDto>> Handle(GetCountryDetailsQuery request, CancellationToken cancellationToken)
            {
                var country = await _CountryReadRepository.GetAsync(x => x.Id == request.Id,
                                                    include: x => x.Include(c => c.WorldRegion));
                if (country == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<CountryDto>
                {
                    Entity = new CountryDto()
                    {
                        CountryId = country.Id,
                        CountryNameAr = country.CountryNameAr,
                        CountryNameEn = country.CountryNameEn,
                        CountryNameLang = country.CountryNameLang,
                        CountryDesc = country.CountryDesc,
                        WorldRegionId = country.WordRegionId,
                        WorldRegionNameAr = country.WorldRegion.WorldRegionNameAr,
                        WorldRegionNameEn = country.WorldRegion.WorldRegionNameEn,
                        WorldRegionNameLang = country.WorldRegion.WorldRegionNameLang,


                        CreatedBy = country.UserId,
                        CreationDate = country.CreatedDate

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetCountryDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}