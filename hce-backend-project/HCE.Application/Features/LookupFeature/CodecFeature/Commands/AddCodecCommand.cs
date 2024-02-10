using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
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


namespace HCE.Application.Features.LookupFeature.CodecFeature.Commands
{
    public class AddCodecCommand : CommandBase<ResponseResult<CodecDto>>
    {
        public string CodecName { get; set; }
        public string CodecDesc { get; set; }



        private class Handler : IRequestHandler<AddCodecCommand, ResponseResult<CodecDto>>
        {
            private readonly IWriteRepository<Codec> _write;
            private readonly IReadRepository<Codec> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Codec> read, IWriteRepository<Codec> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<CodecDto>> Handle(AddCodecCommand request, CancellationToken cancellationToken)
            {
                var codec = new Codec
                {
                    CodecName = request.CodecName,
                    CodecDesc = request.CodecDesc,
                    UserId = _userResolverHandler.GetUserGuid()

                };

                await _write.AddAsync(codec);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<CodecDto>()
                {

                    Entity = new CodecDto()
                    {
                        CodecId = codec.Id,
                        CodecName = codec.CodecName,
                        CodecDesc = codec.CodecDesc,
                        CreationDate = codec.CreatedDate,
                        CreatedBy = codec.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddCodecCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.CodecName).NotEmpty();


                }
            }

        }
    }
}