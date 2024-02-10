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

namespace HCE.Application.Features.LookupFeature.VendorFeature.Queries
{
    public class GetAllVendorDetailsQuery : IRequest<ResponseResult<VendorDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetAllVendorDetailsQuery, ResponseResult<VendorDto>>
        {
            private readonly IReadRepository<Vendor> _ReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Vendor> ReadRepository, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _ReadRepository = ReadRepository;
            }

            public async Task<ResponseResult<VendorDto>> Handle(GetAllVendorDetailsQuery request, CancellationToken cancellationToken)
            {
                var goal = await _ReadRepository.GetAsync(x => x.Id == request.Id
                                                   );
                if (goal == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<VendorDto>
                {
                    Entity = new VendorDto()
                    {
                        VendorId = goal.Id,
                        VendorName = goal.VendorName,
                        VendorDesc = goal.VendorDesc,

                        CreationDate = goal.CreatedDate,
                        CreatedBy = goal.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetAllVendorDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}