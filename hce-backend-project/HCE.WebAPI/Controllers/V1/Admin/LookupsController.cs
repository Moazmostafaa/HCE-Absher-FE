using HCE.Application.Features.LookupFeature.CityFeature.Queries;
using HCE.Application.Features.LookupFeature.CountryFeature.Queries;
using HCE.Application.Features.LookupFeature.DistrictFeature.Queries;
using HCE.Application.Features.LookupFeature.RegionFeature.Commands;
using HCE.Application.Features.LookupFeature.RegionFeature.Queries;
using HCE.Application.Features.LookupFeature.StateRegionFeature.Commands;
using HCE.Application.Features.LookupFeature.StateRegionFeature.Queries;
using HCE.Application.Features.LookupFeature.WorldRegionFeature.Queries;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Lookup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/Lookups")]
    [Route("api/v{version:apiVersion}/admin/Lookups")]
    [Authorize]
    public class LookupsController : ApiControllerBase
    {
        public LookupsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        [Route("GetAllWorldRegions")]
        public async Task<ActionResult<ResponseResult<List<WorldRegionDto>>>> GetAllWorldRegionsWithoutPaginationQuery()
        {
            var query = new GetAllWorldRegionsWithoutPaginationQuery();
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("GetAllCountries")]
        public async Task<ActionResult<ResponseResult<List<CountryDto>>>> GetAllCountries(GetAllCountriesByWorldRegionIdQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("GetAllStateRegions")]
        public async Task<ActionResult<ResponseResult<List<StateRegionDto>>>> GetAllStateRegions(GetAllStateRegionsByCountryIdWithoutPaginationQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("GetAllCities")]
        public async Task<ActionResult<ResponseResult<List<CityDto>>>> GetAllCities(GetAllCitiesByStateRegionWithoutPag query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("GetAllDistricts")]
        public async Task<ActionResult<ResponseResult<List<DistrictDto>>>> GetAllDistricts(GetAllDistrictsByCityIdWithoutPag query)
        {
            return Single(await QueryAsync(query));
        }
    }
}
