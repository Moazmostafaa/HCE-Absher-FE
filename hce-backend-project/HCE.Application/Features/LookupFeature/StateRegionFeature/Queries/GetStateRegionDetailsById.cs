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

namespace HCE.Application.Features.LookupFeature.RegionFeature.Queries
{
    public class GetStateRegionDetailsById : IRequest<ResponseResult<StateRegionDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetStateRegionDetailsById, ResponseResult<StateRegionDto>>
        {
            private readonly IReadRepository<StateRegion> _regionReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<StateRegion> regionReadRepositor, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _regionReadRepository = regionReadRepositor;
            }

            public async Task<ResponseResult<StateRegionDto>> Handle(GetStateRegionDetailsById request, CancellationToken cancellationToken)
            {
                var region = await _regionReadRepository.GetAsync(x => x.Id == request.Id,
                                                    include: x => x.Include(c => c.Country));
                if (region == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<StateRegionDto>
                {
                    Entity = new StateRegionDto()
                    {
                        RegionId = region.Id,
                        RegionNameAr = region.StateRegionNameAr,
                        RegionNameEn = region.StateRegionNameEn,
                        RegionNameLang = region.StateRegionNameLang,
                        RegionDesc = region.StateRegionDesc,
                        CountryId = region.CountryId,
                        CountryNameAr = region.Country.CountryNameAr,
                        CountryNameEn = region.Country.CountryNameEn,

                        CountryNameLang = region.Country.CountryNameLang,

                        CreatedBy = region.UserId,
                        CreationDate = region.CreatedDate

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }

        

            public class Validator : AbstractValidator<GetStateRegionDetailsById>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}