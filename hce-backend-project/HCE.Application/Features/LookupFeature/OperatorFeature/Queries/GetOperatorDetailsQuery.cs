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

namespace HCE.Application.Features.LookupFeature.OperatorFeature.Queries
{
    public class GetOperatorDetailsQuery : IRequest<ResponseResult<OperatorDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetOperatorDetailsQuery, ResponseResult<OperatorDto>>
        {
            private readonly IReadRepository<Operator> _read;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Operator> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }

            public async Task<ResponseResult<OperatorDto>> Handle(GetOperatorDetailsQuery request, CancellationToken cancellationToken)
            {
                var Operator = await _read.GetAsync(x => x.Id == request.Id
                                                 , include: x => x.Include(c => c.OperatorGroup));
                if (Operator == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<OperatorDto>
                {
                    Entity = new OperatorDto()
                    {
                        OperatorGroupId = Operator.OperatorGroup.Id,
                        OperatorGroupName = Operator.OperatorGroup.OperatorGroupName,
                        OperatorName = Operator.OperatorName,
                        OperatorDesc=Operator.OperatorDesc,
                        CreationDate = Operator.CreatedDate,
                        CreatedBy = Operator.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetOperatorDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}