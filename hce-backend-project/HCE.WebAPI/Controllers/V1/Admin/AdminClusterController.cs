using HCE.Application.Features.LookupFeature.CityFeature.Commands;
using HCE.Application.Features.LookupFeature.CityFeature.Queries;
using HCE.Application.Features.LookupFeature.ClusterFeature.Commands;
using HCE.Application.Features.LookupFeature.ClusterFeature.Queries;
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
    [Route("api/admin/Cluster")]
    [Route("api/v{version:apiVersion}/admin/Cluster")]
    [Authorize]
    public class AdminClusterController : ApiControllerBase
    {
        public AdminClusterController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("AddCluster")]

        public async Task<ActionResult<ResponseResult<ClusterDto>>> AddCluster([FromBody] AddClusterCommand command)
        {

            return Single(await CommandAsync(command));

        }
        [HttpPut]
        [Route("UpdateCluster")]

        public async Task<ActionResult<ResponseResult<ClusterDto>>> UpdateCluster([FromBody] UpdateClusterCommand command)
        {

            return Single(await CommandAsync(command));

        }
        [HttpDelete]
        [Route("Delete/{Id}")]

        public async Task<ActionResult<ResponseResult<bool>>> DeleteCluster(DeleteClusterCommand command)
        {

            return Single(await CommandAsync(command));

        }
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<ClusterDto>>>> Search([FromBody] GetAllClustersQuery query)
        {
            return Single(await QueryAsync(query));

        }

        [HttpGet]



        [Route("Get/{Id}")]


        public async Task<ActionResult<ResponseResult<ClusterDto>>> GetCityDetails(GetClusterDetailsQuery query)
        {
            return Single(await QueryAsync(query));
        }

        [HttpPost]
        [Route("GetAllClustersByDistrictId")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<ClusterDto>>>> GetAllClustersByDistrictId([FromBody] GetAllClustersByDistrictIdQuery query)
        {
            return Single(await QueryAsync(query));

        }
    }
}