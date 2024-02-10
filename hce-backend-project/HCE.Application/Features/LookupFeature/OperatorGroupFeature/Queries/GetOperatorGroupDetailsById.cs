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

namespace HCE.Application.Features.LookupFeature.OperatorGroupFeature.Queries
{
    public class GetOperatorGroupDetailsById : IRequest<ResponseResult<OperatorGroupDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetOperatorGroupDetailsById, ResponseResult<OperatorGroupDto>>
        {
            private readonly IReadRepository<OperatorGroup> _read;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<OperatorGroup> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }

            public async Task<ResponseResult<OperatorGroupDto>> Handle(GetOperatorGroupDetailsById request, CancellationToken cancellationToken)
            {
                var operatorGroup = await _read.GetAsync(x => x.Id == request.Id
                                                   );
                if (operatorGroup == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<OperatorGroupDto>
                {
                    Entity = new OperatorGroupDto()
                    {
                        OperatorGroupId = operatorGroup.Id,
                        OperatorGroupName = operatorGroup.OperatorGroupName,
                        OperatorGroupDesc = operatorGroup.OperatorGroupDesc,

                        CreationDate = operatorGroup.CreatedDate,
                        CreatedBy = operatorGroup.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetOperatorGroupDetailsById>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}