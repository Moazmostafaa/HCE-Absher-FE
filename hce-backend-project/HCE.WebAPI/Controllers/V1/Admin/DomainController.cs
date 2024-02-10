using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.DomainFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.DomainFeature.Queries;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/Domain")]
    [Route("api/v{version:apiVersion}/admin/Domain")]
    [Authorize]
    public class DomainController : ApiControllerBase
    {
        public DomainController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddDomain")]
        public async Task<ActionResult<ResponseResult<DomainDto>>> AddDomain([FromBody] AddDomainCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateDomain")]
        public async Task<ActionResult<ResponseResult<DomainDto>>> UpdateDomain([FromBody] UpdateDomainCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteGoal(DeleteDomainCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<DomainDto>>>> GetAllGoals([FromBody] GetAllDomainsQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<DomainDto>>> GetGoalDetails(GetDomainDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}
