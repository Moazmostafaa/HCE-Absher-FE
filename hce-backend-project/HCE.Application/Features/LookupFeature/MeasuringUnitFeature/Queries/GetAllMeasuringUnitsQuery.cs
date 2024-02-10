using FluentValidation;
using HCE.Application.Common;
using HCE.Application.Common.Validators;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Domain.Request;
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
namespace HCE.Application.Features.LookupFeature.MeasuringUnitFeature.Queries
{
    public class GetAllMeasuringUnitsQuery : IRequest<ResponseResult<PagedResponseResult<MeasuringUnitDto>>>, IPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        private class Handler : IRequestHandler<GetAllMeasuringUnitsQuery, ResponseResult<PagedResponseResult<MeasuringUnitDto>>>
        {

            private readonly IReadRepository<MeasuringUnit> _read;

            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<MeasuringUnit> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }
            public async Task<ResponseResult<PagedResponseResult<MeasuringUnitDto>>> Handle(GetAllMeasuringUnitsQuery request, CancellationToken cancellationToken)
            {
                var query = _read.GetManyAsNoTracking();

                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);

                var data = query.OrderByDescending(x => x.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

                var result = new ResponseResult<PagedResponseResult<MeasuringUnitDto>>
                {
                    Entity = new PagedResponseResult<MeasuringUnitDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = data.Select(x => new MeasuringUnitDto()
                        {
                            MeasuringUnitId = x.Id,
                            MeasuringUnitName = x.MeasuringUnitName,
                            MeasuringUnitDesc = x.MeasuringUnitDesc,
                            CreatedBy = x.UserId,
                            CreationDate = x.CreatedDate

                        }).ToList(),
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }


            public class Validator : AbstractValidator<GetAllMeasuringUnitsQuery>
            {
                public Validator()
                {
                    RuleFor(x => x).SetValidator(new PaginationValidator());
                }
            }
        }
    }
}
