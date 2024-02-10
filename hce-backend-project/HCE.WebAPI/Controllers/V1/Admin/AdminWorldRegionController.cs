using HCE.Application.Features.LookupFeature.RegionFeature.Commands;
using HCE.Application.Features.LookupFeature.RegionFeature.Queries;
using HCE.Application.Features.LookupFeature.StateRegionFeature.Commands;
using HCE.Application.Features.LookupFeature.WorldRegionFeature.Commands;
using HCE.Application.Features.LookupFeature.WorldRegionFeature.Queries;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Lookup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/WorldRegion")]
    [Route("api/v{version:apiVersion}/admin/WorldRegion")]
    [Authorize]
    public class AdminWorldRegionController : ApiControllerBase
    {
        public AdminWorldRegionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("AddWorldRegion")]
        public async Task<ActionResult<ResponseResult<WorldRegionDto>>> AddWorldRegion([FromBody] AddWorldRegionCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateWorldRegion")]
        public async Task<ActionResult<ResponseResult<WorldRegionDto>>> UpdateWorldRegion([FromBody] UpdateWorldRegionCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteWorldRegion(DeleteWorldRegionCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<WorldRegionDto>>>> Search([FromBody] GetAllWorldRegionsQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<WorldRegionDto>>> GetWorldRegionDetails(GetWorldRegionDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}