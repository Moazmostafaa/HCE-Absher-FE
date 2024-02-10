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
using HCE.Application.Features.LookupFeature.AccessTechnologyFeature;
using HCE.Application.Features.LookupFeature.AccessTechnologyFeature.Queries;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/AccessTechnolgy")]
    [Route("api/v{version:apiVersion}/admin/AccessTechnolgy")]
    [Authorize]
    public class AccessTechnologyController : ApiControllerBase
    {
        public AccessTechnologyController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("AddAccessTechnolgy")]
        public async Task<ActionResult<ResponseResult<AccessTechnologyDto>>> AddAccessTechnolgy([FromBody] AddAccessTechnologyCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateAccessTechnology")]
        public async Task<ActionResult<ResponseResult<AccessTechnologyDto>>> UpdateAccessTechnology([FromBody] UpdateAccessTechnologyCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteAccessTechnology( DeleteAccessTechnologyCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<AccessTechnologyDto>>>> GetAllAccessTechnologies([FromBody] GetAllAccesTechnologies query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<AccessTechnologyDto>>> GetAccessTechnologyDetails(GetAccessTechnolgyDetails query)
        {
            return Single(await QueryAsync(query));
        }
    }
}