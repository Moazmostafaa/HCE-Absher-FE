using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.DataSourceFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.DataSourceFeature.Queries;
namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/DataSource")]
    [Route("api/v{version:apiVersion}/admin/DataSource")]
    [Authorize]
    public class DataSourceController : ApiControllerBase
    {
        public DataSourceController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [Route("AddDataSource")]
        public async Task<ActionResult<ResponseResult<DataSourceDto>>> AddDataSource([FromBody] AddDataSourceCommand command)
        {

            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateDataSource")]
        public async Task<ActionResult<ResponseResult<DataSourceDto>>> UpdateDataSource([FromBody] UpdateDataSourceCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteDataSource(DeleteDataSourceCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<DataSourceDto>>>> GetAllDataSources([FromBody] GetAllDataSourcesQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<DataSourceDto>>> GetDataSourceDetails(GetDataSourceDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}