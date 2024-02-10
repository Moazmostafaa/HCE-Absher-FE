using HCE.Application.Common;
using HCE.Domain.Entities.Identity;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Managers;
using HCE.Interfaces.Models.Dto.Auth;
using HCE.Interfaces.Repositories;
using HCE.Resource;
using HCE.Utility.CommonModels;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HCE.Persistence.Repositories.Blob;

namespace HCE.Application.Features.LoginFeature.Queries
{
    public class CompeleteLoginCommand : CommandBase<ResponseResult<LoginResponseDto>>
    {
        public string Code { get; set; }
        public string NationalId { get; set; }

        private class Handler : IRequestHandler<CompeleteLoginCommand, ResponseResult<LoginResponseDto>>
        {
            private readonly IOtpManager _otpManager;
            private readonly AppSetting _appSetting;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserManager _userManager;
            private readonly ITokenManager _tokenManager;
            private readonly IReadRepository<User> _readRepo;
            private readonly IWriteRepository<User> _writeRepo;
            private readonly IWriteBlobRepository _blobRepository;

            public Handler(IOptions<AppSetting> options, IWriteBlobRepository blobRepository, ITokenManager tokenManager, IOtpManager otpManager, IUserManager userManager, IReadRepository<User> userReadRepo, IWriteRepository<User> writeRepo, IUnitOfWork unitOfWork)
            {
                _writeRepo = writeRepo;
                _readRepo = userReadRepo;
                _unitOfWork = unitOfWork;
                _otpManager = otpManager;
                _userManager = userManager;
                _tokenManager = tokenManager;
                _appSetting = options.Value;
                _blobRepository = blobRepository;
            }

            public async Task<ResponseResult<LoginResponseDto>> Handle(CompeleteLoginCommand request, CancellationToken cancellationToken)
            {
                ResponseResult<LoginResponseDto> responseResult = new ResponseResult<LoginResponseDto>();

                var isValid = await _otpManager.VerifyOtp(request.NationalId, request.Code);
                if (!isValid)
                {
                    responseResult.IsSuccess = false;
                    responseResult.Message = Message_Resource.IncorrectOTP;
                    responseResult.Status = System.Net.HttpStatusCode.OK;
                    return responseResult;
                }

                responseResult.IsSuccess = true;
                responseResult.Status = HttpStatusCode.OK;
                return responseResult;
            }
        }

        public class Validator : AbstractValidator<CompeleteLoginCommand>
        {
            public Validator()
            {
                RuleFor(x => x.NationalId).NotEmpty();
            }
        }
    }
}
