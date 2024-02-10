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

namespace HCE.Application.Features.LookupFeature.CoreTypeFeature.Queries
{
    public class GetAllCoreTypeDetailsQuery : IRequest<ResponseResult<CoreTypeDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetAllCoreTypeDetailsQuery, ResponseResult<CoreTypeDto>>
        {
            private readonly IReadRepository<CoreType> _ReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<CoreType> ReadRepositor, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _ReadRepository = ReadRepositor;
            }

            public async Task<ResponseResult<CoreTypeDto>> Handle(GetAllCoreTypeDetailsQuery request, CancellationToken cancellationToken)
            {
                var coreType = await _ReadRepository.GetAsync(x => x.Id == request.Id
                                                   );
                if (coreType == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<CoreTypeDto>
                {
                    Entity = new CoreTypeDto()
                    {
                        NPSKPIWeightId = coreType.Id,
                        NPSKPIWeightName = coreType.NPSKPIWeightName,
                        NPSKPIWeightDesc = coreType.NPSKPIWeightDesc,

                        CreationDate = coreType.CreatedDate,
                        CreatedBy = coreType.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetAllCoreTypeDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}