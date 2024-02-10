using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.DomainFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.DomainFeature.Queries;
using HCE.Application.Features.LookupFeature.OperatorFeature.Commands;
using HCE.Application.Features.LookupFeature.OperatorFeature.Queries;
using HCE.Application.Features.LookupFeature.ServiceFeature.Commands;
using HCE.Application.Features.LookupFeature.ServiceFeature.Queries;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/Service")]
    [Route("api/v{version:apiVersion}/admin/Service")]
    [Authorize]
    public class ServiceController : ApiControllerBase
    {
        public ServiceController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddService")]
        public async Task<ActionResult<ResponseResult<ServiceDto>>> AddService([FromBody] AddServiceCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateService")]
        public async Task<ActionResult<ResponseResult<ServiceDto>>> UpdateService([FromBody] UpdateServiceCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteGoal(DeleteServiceCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<ServiceDto>>>> GetAllServices([FromBody] GetAllServicesQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<ServiceDto>>> GetServiceDetails(GetServiceDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}
