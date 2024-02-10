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

namespace HCE.Application.Features.LookupFeature.ExternalOperatorFeature.Queries
{
    public class GetExternalOperatorDetailsQuery : IRequest<ResponseResult<ExternalOperatorDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetExternalOperatorDetailsQuery, ResponseResult<ExternalOperatorDto>>
        {
            private readonly IReadRepository<ExternalOperator> _read;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<ExternalOperator> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }

            public async Task<ResponseResult<ExternalOperatorDto>> Handle(GetExternalOperatorDetailsQuery request, CancellationToken cancellationToken)
            {
                var externalOperator = await _read.GetAsync(x => x.Id == request.Id
                                                 );
                if (externalOperator == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<ExternalOperatorDto>
                {
                    Entity = new ExternalOperatorDto()
                    {
                        ExternalOperatorId = externalOperator.Id,
                        ExternalOperatorName = externalOperator.ExternalOperatorName,
                        ExternalOperatorDesc = externalOperator.ExternalOperatorDesc,
                        CreationDate = externalOperator.CreatedDate,
                        CreatedBy = externalOperator.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetExternalOperatorDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}