using System.Collections.Generic;
using HCE.Application.Features.LookupFeature.MeasuringUnitFeature.Commands;
using HCE.Application.Features.NPS.Commands;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Models.Dto.NPS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/nps")]
    [Route("api/v{version:apiVersion}/admin/nps")]
    //[Authorize]
    public class NpsController : ApiControllerBase
    {
        public NpsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("CalculateNps")]
        public async Task<ActionResult<ResponseResult<List<NpsCalculationDto>>>> CalculateNps()
        {

            return Single(await CommandAsync(new CalculateKpisNpsCommand()));
        }
    }
}
