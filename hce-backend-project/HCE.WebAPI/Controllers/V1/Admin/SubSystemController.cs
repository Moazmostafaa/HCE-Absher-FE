using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.SubSystemFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.SubSystemFeature.Queries;
namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/SubSystem")]
    [Route("api/v{version:apiVersion}/admin/SubSystem")]
    [Authorize]
    public class SubSystemController : ApiControllerBase
    {
        public SubSystemController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddSubSystem")]
        public async Task<ActionResult<ResponseResult<SubSystemDto>>> AddSubSystem([FromBody] AddSubSystemCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateSubSystem")]
        public async Task<ActionResult<ResponseResult<SubSystemDto>>> UpdateSubSystem([FromBody] UpdateSubSystemCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteSubSystem(DeleteSubSystemCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<SubSystemDto>>>> GetAllSubSystems([FromBody] GetAllSubSystemsQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<SubSystemDto>>> GetSubSystemDetails(GetSubSystemDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}