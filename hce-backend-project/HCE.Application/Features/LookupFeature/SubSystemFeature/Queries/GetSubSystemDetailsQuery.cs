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

namespace HCE.Application.Features.LookupFeature.SubSystemFeature.Queries
{
    public class GetSubSystemDetailsQuery : IRequest<ResponseResult<SubSystemDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetSubSystemDetailsQuery, ResponseResult<SubSystemDto>>
        {
            private readonly IReadRepository<SubSystem> _read;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<SubSystem> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }

            public async Task<ResponseResult<SubSystemDto>> Handle(GetSubSystemDetailsQuery request, CancellationToken cancellationToken)
            {
                var subSystem = await _read.GetAsync(x => x.Id == request.Id
                                                   );
                if (subSystem == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<SubSystemDto>
                {
                    Entity = new SubSystemDto()
                    {
                        SubSystemId = subSystem.Id,
                        SubSystemName = subSystem.SubSystemName,
                        SubSystemDesc = subSystem.SubSystemDesc,

                        CreationDate = subSystem.CreatedDate,
                        CreatedBy = subSystem.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetSubSystemDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}