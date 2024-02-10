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
    public class GetCityDetailsByIdQuery : IRequest<ResponseResult<CityDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetCityDetailsByIdQuery, ResponseResult<CityDto>>
        {
            private readonly IReadRepository<City> _CityyReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<City> CityReadRepositor, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _CityyReadRepository = CityReadRepositor;
            }

            public async Task<ResponseResult<CityDto>> Handle(GetCityDetailsByIdQuery request, CancellationToken cancellationToken)
            {
                var city = await _CityyReadRepository.GetAsync(x => x.Id == request.Id,
                                                    include: x => x.Include(c => c.StateRegion));
                if (city == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<CityDto>
                {
                    Entity = new CityDto()
                    {
                        CityId=city.Id,
                        CityNameAr = city.CityNameAr,
                        CityNameEn = city.CityNameEn,
                        CityNameLang = city.CityNameLang,
                        CityDesc = city.CityDesc,
                        CreatedBy = _userResolverHandler.GetUserGuid(),
                        CreationDate = city.CreatedDate,
                        StateRegionId = city.StateRegionId,
                        IsTop = city.IsTop,
                        StateRegionNameAr = city.StateRegion.StateRegionNameAr,
                        StateRegionNameEn = city.StateRegion.StateRegionNameEn,
                        StateRegionNameLang = city.StateRegion.StateRegionNameLang

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetCityDetailsByIdQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}