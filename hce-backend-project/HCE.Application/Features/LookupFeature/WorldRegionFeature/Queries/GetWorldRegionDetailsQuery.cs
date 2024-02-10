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

namespace HCE.Application.Features.LookupFeature.WorldRegionFeature.Queries
{
    public class GetWorldRegionDetailsQuery : IRequest<ResponseResult<WorldRegionDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetWorldRegionDetailsQuery, ResponseResult<WorldRegionDto>>
        {
            private readonly IReadRepository<WorldRegion> _regionReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<WorldRegion> regionReadRepositor, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _regionReadRepository = regionReadRepositor;
            }

            public async Task<ResponseResult<WorldRegionDto>> Handle(GetWorldRegionDetailsQuery request, CancellationToken cancellationToken)
            {
                var region = await _regionReadRepository.GetAsync(x => x.Id == request.Id);
                                                   
                if (region == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<WorldRegionDto>
                {
                    Entity = new WorldRegionDto()
                    {
                        RegionId = region.Id,
                        RegionNameAr = region.WorldRegionNameAr,
                        RegionNameEn = region.WorldRegionNameEn,
                        RegionNameLang = region.WorldRegionNameLang,
                        RegionDesc = region.WorldRegionDesc,
               

                        CreatedBy = region.UserId,
                        CreationDate = region.CreatedDate

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetWorldRegionDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}