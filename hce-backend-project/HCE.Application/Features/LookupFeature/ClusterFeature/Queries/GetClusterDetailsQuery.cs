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
namespace HCE.Application.Features.LookupFeature.ClusterFeature.Queries
{
    public class GetClusterDetailsQuery : IRequest<ResponseResult<ClusterDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetClusterDetailsQuery, ResponseResult<ClusterDto>>
        {
            private readonly IReadRepository<Cluster> _ReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Cluster> ReadRepositor, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _ReadRepository = ReadRepositor;
            }

            public async Task<ResponseResult<ClusterDto>> Handle(GetClusterDetailsQuery request, CancellationToken cancellationToken)
            {
                var cluster = await _ReadRepository.GetAsync(x => x.Id == request.Id,
                                                    include: x => x.Include(c => c.District));
                if (cluster == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<ClusterDto>
                {
                    Entity = new ClusterDto()
                    {
                        ClusterId=cluster.Id,
                        ClusterNameAr=cluster.ClusterNameAr,
                        ClusterNameEn=cluster.ClusterNameEn,
                        ClusterNameLang=cluster.ClusterNameLang,
                        ClusterDesc=cluster.ClusterDesc,
                        DistrictId = cluster.District.Id,
                        DistrictNameAr = cluster.District.DistrictNameAr,
                        DistrictNameEn = cluster.District.DistrictNameEn,
                        DistrictNameLang = cluster.District.DistrictNameLang,
                       


                        CreationDate = cluster.CreatedDate,
                        CreatedBy = cluster.UserId

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetClusterDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}