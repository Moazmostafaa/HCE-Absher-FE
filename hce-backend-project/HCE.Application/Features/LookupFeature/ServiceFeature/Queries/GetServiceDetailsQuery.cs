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

namespace HCE.Application.Features.LookupFeature.ServiceFeature.Queries
{
    public class GetServiceDetailsQuery : IRequest<ResponseResult<ServiceDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetServiceDetailsQuery, ResponseResult<ServiceDto>>
        {
            private readonly IReadRepository<Service> _ReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Service> ReadRepositor, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _ReadRepository = ReadRepositor;
            }

            public async Task<ResponseResult<ServiceDto>> Handle(GetServiceDetailsQuery request, CancellationToken cancellationToken)
            {
                var service = await _ReadRepository.GetAsync(x => x.Id == request.Id
                                                   );
                if (service == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<ServiceDto>
                {
                    Entity = new ServiceDto()
                    {
                        ServiceId = service.Id,
                        ServiceName = service.ServiceName,
                        ServiceDesc = service.ServiceDesc,

                        CreationDate = service.CreatedDate,
                        CreatedBy = service.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetServiceDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}