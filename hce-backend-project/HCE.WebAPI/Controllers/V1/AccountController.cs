using HCE.Application.Features.LoginFeature.Command;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HCE.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        public AccountController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ResponseResult<LoginResponseDto>>> Login(BasicLoginCommand login)
        {
            return Single(await CommandAsync(login));
        }
    }
}
