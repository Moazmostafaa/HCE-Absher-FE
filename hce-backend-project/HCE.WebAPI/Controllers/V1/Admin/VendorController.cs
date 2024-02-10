using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using HCE.Application.Features.LookupFeature.GoalFeature.Commands;
using HCE.Interfaces.Models.Dto.Lookup;
using System.Threading.Tasks;
using HCE.Domain.ResponseModel;
using HCE.Application.Features.LookupFeature.GoalFeature.Queries;
using HCE.Application.Features.LookupFeature.VendorFeature.Commands;
using HCE.Application.Features.LookupFeature.VendorFeature.Queries;

namespace HCE.WebAPI.Controllers.V1.Admin
{
    [ApiVersion("1.0")]
    [Route("api/admin/Vendor")]
    [Route("api/v{version:apiVersion}/admin/Vendor")]
    [Authorize]
    public class VendorController : ApiControllerBase
    {
        public VendorController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("AddVendor")]
        public async Task<ActionResult<ResponseResult<VendorDto>>> AddVendor([FromBody] AddVendorCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPut]
        [Route("UpdateVendor")]
        public async Task<ActionResult<ResponseResult<VendorDto>>> UpdateVendor([FromBody] UpdateVendorCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult<ResponseResult<bool>>> DeleteVendor(DeleteVendorCommand command)
        {
            return Single(await CommandAsync(command));
        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<VendorDto>>>> GetAllVendors([FromBody] GetAllVendorsQuery query)
        {
            return Single(await QueryAsync(query));
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<ActionResult<ResponseResult<VendorDto>>> GetVendorDetails(GetAllVendorDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }
    }
}
