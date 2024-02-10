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
using HCE.Application.Features.LookupFeature.ExternalOperatorFeature.Commands;
using HCE.Application.Features.LookupFeature.ExternalOperatorFeature.Queries;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/ExternalOperator")]
    [Route("api/v{version:apiVersion}/admin/ExternalOperator")]
    [Authorize]
    public class ExternalOperatorController : ApiControllerBase
    {
        public ExternalOperatorController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddExternalOperator")]
        public async Task<ActionResult<ResponseResult<ExternalOperatorDto>>> AddExternalOperator([FromBody] AddExternalOperatorCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateExternalOperator")]
        public async Task<ActionResult<ResponseResult<ExternalOperatorDto>>> UpdateExternalOperator([FromBody] UpdateExternalOperatorCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteGoal(DeleteExternalOperatorComman command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<ExternalOperatorDto>>>> GetAllExternalOperators([FromBody] GetAllExternalOperatorsQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<ExternalOperatorDto>>> GetExternalOperatorDetails(GetExternalOperatorDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}