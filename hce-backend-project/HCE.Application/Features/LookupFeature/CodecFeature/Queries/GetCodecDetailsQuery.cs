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

namespace HCE.Application.Features.LookupFeature.CodecFeature.Queries
{
    public class GetCodecDetailsQuery : IRequest<ResponseResult<CodecDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetCodecDetailsQuery, ResponseResult<CodecDto>>
        {
            private readonly IReadRepository<Codec> _read;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<Codec> read, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _read = read;
            }

            public async Task<ResponseResult<CodecDto>> Handle(GetCodecDetailsQuery request, CancellationToken cancellationToken)
            {
                var  codec = await _read.GetAsync(x => x.Id == request.Id
                                                   );
                if (codec == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<CodecDto>
                {
                    Entity = new CodecDto()
                    {
                        CodecId = codec.Id,
                        CodecName = codec.CodecName,
                        CodecDesc = codec.CodecDesc,

                        CreationDate = codec.CreatedDate,
                        CreatedBy = codec.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetCodecDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}