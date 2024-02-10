using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.GoalFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.GoalFeature.Queries;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/Goal")]
    [Route("api/v{version:apiVersion}/admin/Goal")]
    [Authorize]
    public class GoalController : ApiControllerBase
    {
        public GoalController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddGoal")]
        public async Task<ActionResult<ResponseResult<GoalDto>>> AddGoal([FromBody] AddGoalCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateGoal")]
        public async Task<ActionResult<ResponseResult<GoalDto>>> UpdateGoal([FromBody] UpdateGoalCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteGoal( DeleteGoalCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<GoalDto>>>> GetAllGoals([FromBody] GetAllGoalsQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<GoalDto>>> GetGoalDetails( GetGoalDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}
