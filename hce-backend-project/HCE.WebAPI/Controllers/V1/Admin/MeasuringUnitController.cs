using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.MeasuringUnitFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.MeasuringUnitFeature.Queries;
namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/MeasuringUnit")]
    [Route("api/v{version:apiVersion}/admin/MeasuringUnit")]
    [Authorize]
    public class MeasuringUnitController : ApiControllerBase
    {
        public MeasuringUnitController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddMeasuringUnit")]
        public async Task<ActionResult<ResponseResult<MeasuringUnitDto>>> AddMeasuringUnit([FromBody] AddMeasuringUnitCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateMeasuringUnit")]
        public async Task<ActionResult<ResponseResult<MeasuringUnitDto>>> UpdateMeasuringUnit([FromBody] UpdateMeasuringUnitCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteDataSource(DeleteMeasuringUnitCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<MeasuringUnitDto>>>> GetAllMeasuringUnits([FromBody] GetAllMeasuringUnitsQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<MeasuringUnitDto>>> GetAllMeasuringUnitDetails(GetMeasuringUnitDetails query)
        {
            return Single(await QueryAsync(query));
        }
    }
}