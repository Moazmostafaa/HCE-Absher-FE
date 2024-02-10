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
    public class GetMeasuringUnitDetails : IRequest<ResponseResult<MeasuringUnitDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetMeasuringUnitDetails, ResponseResult<MeasuringUnitDto>>
        {
            private readonly IReadRepository<MeasuringUnit> _read;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<MeasuringUnit> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }

            public async Task<ResponseResult<MeasuringUnitDto>> Handle(GetMeasuringUnitDetails request, CancellationToken cancellationToken)
            {
                var measuringUnit = await _read.GetAsync(x => x.Id == request.Id
                                                   );
                if (measuringUnit == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<MeasuringUnitDto>
                {
                    Entity = new MeasuringUnitDto()
                    {
                        MeasuringUnitId = measuringUnit.Id,
                        MeasuringUnitName = measuringUnit.MeasuringUnitName,
                        MeasuringUnitDesc = measuringUnit.MeasuringUnitDesc,

                        CreationDate = measuringUnit.CreatedDate,
                        CreatedBy = measuringUnit.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetMeasuringUnitDetails>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}