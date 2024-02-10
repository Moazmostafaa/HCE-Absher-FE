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

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/Operator")]
    [Route("api/v{version:apiVersion}/admin/Operator")]
    [Authorize]
    public class OperatorController : ApiControllerBase
    {
        public OperatorController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddOperator")]
        public async Task<ActionResult<ResponseResult<OperatorDto>>> AddOperator([FromBody] AddOperatorCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateOperator")]
        public async Task<ActionResult<ResponseResult<OperatorDto>>> UpdateOperator([FromBody] UpdateOperatorCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteGoal(DeleteOperatorCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<OperatorDto>>>> GetAllOperators([FromBody] GetOperatorsQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<OperatorDto>>> GetOperatorDetails(GetOperatorDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}
