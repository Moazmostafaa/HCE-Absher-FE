using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
using HCE.Utility.Extensions;
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
    public class UpdateCodecCommand : CommandBase<ResponseResult<CodecDto>>
    {
        public Guid CodecId { get; set; }
        public string CodecName { get; set; }
        public string CodecDesc { get; set; }


        private class Handler : IRequestHandler<UpdateCodecCommand, ResponseResult<CodecDto>>
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

            public async Task<ResponseResult<CodecDto>> Handle(UpdateCodecCommand request, CancellationToken cancellationToken)
            {
                var codec = await _read.GetAsync(x => x.Id == request.CodecId);
                if (codec == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                codec.CodecName = request.CodecName;
                codec.CodecDesc = request.CodecDesc;
                codec.UpdatedBy = _userResolverHandler.GetUserId();
                codec.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                _write.Update(codec);

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


            public class Validator : AbstractValidator<UpdateCodecCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.CodecId).NotEmpty();

                    RuleFor(x => x.CodecName).NotEmpty();


                }
            }

        }
    }
}
