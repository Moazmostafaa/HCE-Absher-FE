using FluentValidation;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.LookupFeature.AccessTechnologyFeature.Queries
{
    public class GetAccessTechnolgyDetails : IRequest<ResponseResult<AccessTechnologyDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetAccessTechnolgyDetails, ResponseResult<AccessTechnologyDto>>
        {
            private readonly IReadRepository<AccessTechnology> _readRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<AccessTechnology> readRepository, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _readRepository = readRepository;
            }

            public async Task<ResponseResult<AccessTechnologyDto>> Handle(GetAccessTechnolgyDetails request, CancellationToken cancellationToken)
            {
                var accessTechnology = await _readRepository.GetAsync(x => x.Id == request.Id
                                                   );
                if (accessTechnology == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<AccessTechnologyDto>
                {
                    Entity = new AccessTechnologyDto()
                    {
                        ServiceId = accessTechnology.Id,
                        ServiceName = accessTechnology.ServiceName,
                        ServiceDesc = accessTechnology.ServiceDesc,

                        CreationDate = accessTechnology.CreatedDate,
                        CreatedBy = accessTechnology.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;
            }
            public class Validator : AbstractValidator<GetAccessTechnolgyDetails>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}