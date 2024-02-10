using HCE.Application.Features.LookupFeature.CityFeature.Commands;
using HCE.Application.Features.LookupFeature.CityFeature.Queries;
using HCE.Application.Features.LookupFeature.CountryFeature.Commands;
using HCE.Application.Features.LookupFeature.CountryFeature.Queries;
using HCE.Application.Features.LookupFeature.DistrictFeature.Commands;
using HCE.Application.Features.LookupFeature.DistrictFeature.Queries;
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
    [Route("api/admin/District")]
    [Route("api/v{version:apiVersion}/admin/District")]
    [Authorize]
    public class AdminDistrictController : ApiControllerBase
    {
        public AdminDistrictController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("AddDistrict")]

        public async Task<ActionResult<ResponseResult<DistrictDto>>> AddDistrict([FromBody] AddDistrictCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateDistrict")]
        public async Task<ActionResult<ResponseResult<DistrictDto>>> UpdateDistrict([FromBody] UpdateDistrictCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteDistrict(DeleteDistrictCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<DistrictDto>>>> Search([FromBody] GetAllDistrictsQuery query)
        {
            return Single(await QueryAsync(query));
        }

        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<DistrictDto>>> GetDistrictDetails(GetDistrictDetailsByIdQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpPost]
        [Route("GetAllDistrictsByCityId")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<DistrictDto>>>> GetAllDistrictsByCityId([FromBody] GetAllDistrictsByCityId query)
        {
            return Single(await QueryAsync(query));
        }
    }
}