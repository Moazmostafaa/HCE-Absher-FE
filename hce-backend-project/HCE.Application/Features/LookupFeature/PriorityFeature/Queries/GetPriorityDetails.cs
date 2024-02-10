﻿using FluentValidation;
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

namespace HCE.Application.Features.LookupFeature.PriorityFeature.Queries
{
    public class GetPriorityDetails : IRequest<ResponseResult<PriorityDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetPriorityDetails, ResponseResult<PriorityDto>>
        {
            private readonly IReadRepository<Priority> _read;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Priority> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }

            public async Task<ResponseResult<PriorityDto>> Handle(GetPriorityDetails request, CancellationToken cancellationToken)
            {
                var priority = await _read.GetAsync(x => x.Id == request.Id
                                                   );
                if (priority == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<PriorityDto>
                {
                    Entity = new PriorityDto()
                    {
                        PriorityId = priority.Id,
                        PriorityName = priority.PriorityName,
                        PriorityDesc = priority.PriorityDesc,

                        CreationDate = priority.CreatedDate,
                        CreatedBy = priority.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetPriorityDetails>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}