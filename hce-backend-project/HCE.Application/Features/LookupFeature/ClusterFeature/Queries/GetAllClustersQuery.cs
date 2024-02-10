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
    public class GetAllClustersQuery : IRequest<ResponseResult<PagedResponseResult<ClusterDto>>>, IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllClustersQuery, ResponseResult<PagedResponseResult<ClusterDto>>>
        {

            private readonly IReadRepository<Cluster> _ReadRepository;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Cluster> ReadRepository, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _ReadRepository = ReadRepository;
            }
            public async Task<ResponseResult<PagedResponseResult<ClusterDto>>> Handle(GetAllClustersQuery request, CancellationToken cancellationToken)
            {
                var query = _ReadRepository.GetManyAsNoTracking(x => x.IsDeleted == false,
                                                  include: x => x.Include(c => c.District));

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<ClusterDto>>
                {
                    Entity = new PagedResponseResult<ClusterDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new ClusterDto()
                        {
                            ClusterId = x.Id,
                            ClusterNameAr = x.ClusterNameAr,
                            ClusterNameEn = x.ClusterNameEn,
                            ClusterNameLang = x.ClusterNameLang,
                            ClusterDesc = x.ClusterDesc,
                            DistrictId = x.District.Id,
                            DistrictNameAr = x.District.DistrictNameAr,
                            DistrictNameEn = x.District.DistrictNameEn,
                            DistrictNameLang = x.District.DistrictNameLang,


                            CreationDate = x.CreatedDate,
                            CreatedBy = x.UserId



                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllClustersQuery>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}