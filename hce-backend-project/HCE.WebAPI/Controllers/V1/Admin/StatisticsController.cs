using System.Collections.Generic;
using System.Threading.Tasks;
using HCE.Application.Features.Statistics.Queries;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Statistics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/Statistics")]
    [Route("api/v{version:apiVersion}/admin/Statistics")]
    [Authorize]
    public class StatisticsController : ApiControllerBase
    {
        public StatisticsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("Nps")]
        public async Task<ActionResult<ResponseResult<OverallNpsDto>>> OverallNps([FromBody] OverallNpsQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpPost]
        [Route("TopCitiesNps")]
        public async Task<ActionResult<ResponseResult<List<TopCityNpsDto>>>> TopCitiesNps([FromBody] TopCitiesNpsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}
