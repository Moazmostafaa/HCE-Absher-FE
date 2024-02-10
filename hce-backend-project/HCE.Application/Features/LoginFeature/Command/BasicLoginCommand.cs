using System.Threading;
using System.Threading.Tasks;
using HCE.Domain.Entities.Identity;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Managers;
using HCE.Interfaces.Models.Dto.Auth;
using HCE.Interfaces.Repositories;
using MediatR;

namespace HCE.Application.Features.LoginFeature.Command
{
    public class BasicLoginCommand : IRequest<ResponseResult<LoginResponseDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public class BasicLoginCommandHandler : IRequestHandler<BasicLoginCommand, ResponseResult<LoginResponseDto>>
        {

            private readonly IUserManager _userManager;
            private readonly ITokenManager _tokenManager;
            private readonly IReadRepository<User> _userReadRepository;
            private readonly IWriteRepository<User> _userWriteRepository;

            public BasicLoginCommandHandler(IUserManager userManager, ITokenManager tokenManager, IWriteRepository<User> userWriteRepository, IReadRepository<User> userReadRepository)
            {
                _userManager = userManager;
                _tokenManager = tokenManager;
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
            }

            public async Task<ResponseResult<LoginResponseDto>> Handle(BasicLoginCommand request, CancellationToken cancellationToken)
            {
                ResponseResult<LoginResponseDto> responseResult = new ResponseResult<LoginResponseDto>();

                var user = await _userManager.GetDtoByUserNameAndPassword(request.UserName, request.Password);

                var token = _tokenManager.GenerateToken(user.UserId, user.FullName, user.PhoneNumber, user.UserName, user.ProfileImageId);
                await _tokenManager.CreateOrUpdateToken(user.UserId, token);
                var dept = "";
                var grand = "";

                responseResult.Entity = new LoginResponseDto()
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    ProfileAttachmentId = user.ProfileImageId,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Token = token,
                    Grand = grand,
                    Department = dept
                };
                responseResult.IsSuccess = true;
                responseResult.Status = System.Net.HttpStatusCode.OK;

                return responseResult;
            }
        }
    }
}
