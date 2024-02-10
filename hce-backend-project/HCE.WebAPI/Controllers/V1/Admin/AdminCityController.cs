using HCE.Application.Features.LookupFeature.CityFeature.Commands;
using HCE.Application.Features.LookupFeature.CityFeature.Queries;
using HCE.Application.Features.LookupFeature.CountryFeature.Commands;
using HCE.Application.Features.LookupFeature.CountryFeature.Queries;
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
    [Route("api/admin/City")]
    [Route("api/v{version:apiVersion}/admin/City")]
    [Authorize]
    public class AdminCityController : ApiControllerBase
    {
        public AdminCityController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("AddCity")]
        public async Task<ActionResult<ResponseResult<CityDto>>> AddCity([FromBody] AddCityFeatureCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateCity")]
        public async Task<ActionResult<ResponseResult<CityDto>>> UpdateCity([FromBody] UpdateCityCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteCity(DeleteCityCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<CityDto>>>> Search([FromBody] GetAllCitiesQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<CityDto>>> GetCityDetails(GetCityDetailsByIdQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpPost]
        [Route("GetCitiesByStateRegionId")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<CityDto>>>> GetCitiesByStateRegionId([FromBody] GetAllCitiesByStateRegionIdQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}