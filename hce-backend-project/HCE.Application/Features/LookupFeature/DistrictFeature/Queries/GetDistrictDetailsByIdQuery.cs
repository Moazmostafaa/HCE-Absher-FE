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
    public class GetDistrictDetailsByIdQuery : IRequest<ResponseResult<DistrictDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetDistrictDetailsByIdQuery, ResponseResult<DistrictDto>>
        {
            private readonly IReadRepository<District> _ReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<District> ReadRepositor, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _ReadRepository = ReadRepositor;
            }

            public async Task<ResponseResult<DistrictDto>> Handle(GetDistrictDetailsByIdQuery request, CancellationToken cancellationToken)
            {
                var district = await _ReadRepository.GetAsync(x => x.Id == request.Id,
                                                    include: x => x.Include(c => c.City));
                if (district == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<DistrictDto>
                {
                    Entity = new DistrictDto()
                    {
                        DistrictId = district.Id,
                        DistrictNameAr = district.DistrictNameAr,
                        DistrictNameEn = district.DistrictNameEn,
                        DistrictNameLang = district.DistrictNameLang,
                        DistrictDesc = district.DistrictDesc,
                        CityId = district.City.Id,
                        CityNameAr = district.City.CityNameAr,
                        CityNameEn = district.City.CityNameEn,
                        CityNameLang = district.City.CityNameLang,


                        CreationDate = district.CreatedDate,
                        CreatedBy = district.UserId

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetDistrictDetailsByIdQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}