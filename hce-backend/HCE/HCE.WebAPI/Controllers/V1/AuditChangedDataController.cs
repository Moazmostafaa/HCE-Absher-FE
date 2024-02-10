using HCE.Application.Features.AuditChangedDataFeature.Queries;
using HCE.Domain.Entities.Audit;
using HCE.Domain.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCE.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuditChangedDataController : ApiControllerBase
    {
        public AuditChangedDataController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("GetAuditChangedDataAsync")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<AuditChangedData>>>> GetAuditChangedDataAsync([FromQuery] GetAuditChangedDataCommand getAuditChangedDataCommand)
        {
            return Single(await QueryAsync(getAuditChangedDataCommand));
        }
    }
}
