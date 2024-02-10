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
    [Route("api/admin/Country")]
    [Route("api/v{version:apiVersion}/admin/Country")]
    [Authorize]
    public class AdminCountryController : ApiControllerBase
    {
        public AdminCountryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("AddCountry")]
        public async Task<ActionResult<ResponseResult<CountryDto>>> AddCountry([FromBody] AddCountryCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateCountry")]
        public async Task<ActionResult<ResponseResult<CountryDto>>> UpdateCountry([FromBody] UpdateCountryCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteCountry(DeleteCountryCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<CountryDto>>>> Search([FromBody] GetAllCountriesQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<CountryDto>>> GetCountryDetails(GetCountryDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}