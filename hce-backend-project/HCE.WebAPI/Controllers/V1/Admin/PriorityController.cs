using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.PriorityFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.PriorityFeature.Queries;
namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/Priority")]
    [Route("api/v{version:apiVersion}/admin/Priority")]
    [Authorize]
    public class PriorityController : ApiControllerBase
    {

        public PriorityController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddPriority")]
        public async Task<ActionResult<ResponseResult<PriorityDto>>> AddPriority([FromBody] AddPriorityFeatureCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdatePriority")]
        public async Task<ActionResult<ResponseResult<PriorityDto>>> UpdatePriority([FromBody] UpdatePriorityCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeletePriority(DeletePriorityCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<PriorityDto>>>> GetAllPriorities([FromBody] GetAllPrioritiesQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<PriorityDto>>> GetPriorityDetails(GetPriorityDetails query)
        {
            return Single(await QueryAsync(query));
        }
    }
}