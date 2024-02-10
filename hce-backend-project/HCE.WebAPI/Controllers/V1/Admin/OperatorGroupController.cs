using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.OperatorGroupFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.OperatorGroupFeature.Queries;
namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/OperatorGroup")]
    [Route("api/v{version:apiVersion}/admin/OperatorGroup")]
    [Authorize]
    public class OperatorGroupController : ApiControllerBase
    {
        public OperatorGroupController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddOperatorGroup")]
        public async Task<ActionResult<ResponseResult<OperatorGroupDto>>> AddOperatorGroup([FromBody] AddOperatorGroupCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateOperatorGroup")]
        public async Task<ActionResult<ResponseResult<OperatorGroupDto>>> UpdateOperatorGroup([FromBody] UpdateOperatorGroupCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteOperatorGroup(DeleteOperatorGroupCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<OperatorGroupDto>>>> GetAllOperatorGroups([FromBody] GetAllOperatorGroupsQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<OperatorGroupDto>>> GetOperatorGroupDetailsById(GetOperatorGroupDetailsById query)
        {
            return Single(await QueryAsync(query));
        }
    }
}