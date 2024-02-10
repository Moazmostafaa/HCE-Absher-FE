using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.GoalFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.GoalFeature.Queries;
using HCE.Application.Features.LookupFeature.CoreTypeFeature.Commands;
using HCE.Application.Features.LookupFeature.CoreTypeFeature.Queries;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/CoreType")]
    [Route("api/v{version:apiVersion}/admin/CoreType")]
    [Authorize]
    public class CoreTypeController : ApiControllerBase
    {
        public CoreTypeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("AddCoreType")]
        public async Task<ActionResult<ResponseResult<CoreTypeDto>>> AddCoreType([FromBody] AddCoreTypeCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateCoreType")]
        public async Task<ActionResult<ResponseResult<CoreTypeDto>>> UpdateCoreType([FromBody] UpdateCoreTypeCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteCoreType( DeleteCoreTypeCommand command)
        {
            return Single(await CommandAsync(command));
        }

        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<CoreTypeDto>>>> GetAllCoreTypes([FromBody] GetAllCoreTypesQuery query)
        {
            return Single(await QueryAsync(query));
        }

        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<CoreTypeDto>>> GetAllCoreTypeDetails(GetAllCoreTypeDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}