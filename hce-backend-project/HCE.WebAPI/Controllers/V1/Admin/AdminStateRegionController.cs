using HCE.Application.Features.LookupFeature.RegionFeature.Commands;
using HCE.Application.Features.LookupFeature.RegionFeature.Queries;
using HCE.Application.Features.LookupFeature.StateRegionFeature.Commands;
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
    [Route("api/admin/StateRegion")]
    [Route("api/v{version:apiVersion}/admin/StateRegion")]
    [Authorize]
    public class AdminStateRegionController : ApiControllerBase
    {
        public AdminStateRegionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("AddRegion")]
        public async Task<ActionResult<ResponseResult<StateRegionDto>>> AddStateRegion([FromBody] AddRegionCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateRegion")]
        public async Task<ActionResult<ResponseResult<StateRegionDto>>> UpdateRegion([FromBody] UpdateRegionCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteStateRegion( DeleteRegionCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<StateRegionDto>>>> Search([FromBody] GetAllStateRegions query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpPost]
        [Route("GetStateRegionsByCountryId")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<StateRegionDto>>>> GetStateRegionsByCountryId([FromBody] GetAllRegionsByCountryId query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<StateRegionDto>>> GetRegionDetails(GetStateRegionDetailsById query)
        {
            return Single(await QueryAsync(query));
        }
    }
}
