using HCE.Domain.ResponseModel;
using HCE.Interfaces.Managers;
using HCE.Application.Common;
using HCE.Resource;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HCE.Utility.Exceptions;

namespace HCE.Application.Features.LoginFeature.Queries
{
    public class LoginExtendedCommand : QueryBase<ResponseResult<bool>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClientIpAddress { get; set; }

        private class LoginExtendedHandler : IRequestHandler<LoginExtendedCommand, ResponseResult<bool>>
        {
            private readonly IUserManager _userManager;
            private readonly IOtpManager _otpManager;

            public LoginExtendedHandler(IOtpManager otpManager, IUserManager userManager)
            {
                _otpManager = otpManager;
                _userManager = userManager;
            }

            public async Task<ResponseResult<bool>> Handle(LoginExtendedCommand request, CancellationToken cancellationToken)
            {
                ResponseResult<bool> responseResult = new ResponseResult<bool>();
                request.UserName = request.UserName.Trim();
                request.Password = request.Password.Trim();
                request.ClientIpAddress = request.ClientIpAddress.Trim();

                var user = await _userManager.GetDtoByUserNameOrNID(request.UserName);
                if (user == null)
                {
                    responseResult.Entity = false;
                    responseResult.IsSuccess = false;
                    responseResult.Message = Message_Resource.FieldInLoginOperation;
                    responseResult.Status = HttpStatusCode.OK;
                    return responseResult;
                }

                var optResult = await _otpManager.SendOtp(request.UserName);
                if (!optResult)
                    throw new BusinessException(Message_Resource.UnableToSendOtp);

                responseResult.Entity = true;
                responseResult.IsSuccess = true;
                responseResult.Status = HttpStatusCode.OK;

                return responseResult;
            }
        }
    }
}
