using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.CodecFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.CodecFeature.Queries;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/Codec")]
    [Route("api/v{version:apiVersion}/admin/Codec")]
    [Authorize]
    public class CodecController : ApiControllerBase
    {
        public CodecController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddCodec")]
        public async Task<ActionResult<ResponseResult<CodecDto>>> AddCodec([FromBody] AddCodecCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateCodec")]
        public async Task<ActionResult<ResponseResult<CodecDto>>> UpdateDomain([FromBody] UpdateCodecCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteGoal(DeleteCodecCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<CodecDto>>>> GetAllGoals([FromBody] GetAllCodecQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<CodecDto>>> GetGoalDetails(GetCodecDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}