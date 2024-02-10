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
namespace HCE.Application.Features.LookupFeature.DomainFeature.Queries
{
    public class GetDomainDetailsQuery : IRequest<ResponseResult<DomainDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetDomainDetailsQuery, ResponseResult<DomainDto>>
        {
            private readonly IReadRepository<Domains> _read;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Domains> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }

            public async Task<ResponseResult<DomainDto>> Handle(GetDomainDetailsQuery request, CancellationToken cancellationToken)
            {
                var domain = await _read.GetAsync(x => x.Id == request.Id
                                                   );
                if (domain == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<DomainDto>
                {
                    Entity = new DomainDto()
                    {
                        DomainId = domain.Id,
                        DomainName = domain.DomainName,
                        DomainDesc = domain.DomainDesc,

                        CreationDate = domain.CreatedDate,
                        CreatedBy = domain.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetDomainDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}