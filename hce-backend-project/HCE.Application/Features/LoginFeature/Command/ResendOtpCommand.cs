using System.Threading;
using System.Threading.Tasks;
using HCE.Application.Common;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Managers;
using FluentValidation;
using MediatR;

namespace HCE.Application.Features.LoginFeature.Command
{
    public class ResendOtpCommand : CommandBase<ResponseResult<bool>>
    {
        public string NationalId { get; set; }
        private class Handler : IRequestHandler<ResendOtpCommand, ResponseResult<bool>>
        {
            private readonly IOtpManager _otpManager;
            public Handler(IOtpManager otpManager)
            {
                _otpManager = otpManager;
            }
            public async Task<ResponseResult<bool>> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
            {
                var result = await _otpManager.ResendOtp(request.NationalId);
                return new ResponseResult<bool>(result);
            }
        }
        public class Validator : AbstractValidator<ResendOtpCommand>
        {
            public Validator()
            {
                RuleFor(x => x.NationalId).NotEmpty();
            }
        }
    }
}
