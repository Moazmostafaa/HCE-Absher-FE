using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using HCE.Domain.Entities.NPS;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Statistics;
using HCE.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HCE.Application.Features.Statistics.Queries
{
    public class OverallNpsQuery : IRequest<ResponseResult<OverallNpsDto>>
    {
        public DateTime Date { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? StateRegionId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? ClusterId { get; set; }
        public Guid? SiteId { get; set; }
        public Guid? CellId { get; set; }

        internal class Handler : IRequestHandler<OverallNpsQuery, ResponseResult<OverallNpsDto>>
        {
            private readonly IReadRepository<NpsResult> _readRepository;
            public Handler(IReadRepository<NpsResult> readRepository)
            {
                _readRepository = readRepository;
            }
            public async Task<ResponseResult<OverallNpsDto>> Handle(OverallNpsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<NpsResult, bool>> expression =
                    x =>
                        (x.CreatedDate.Date == request.Date.Date) &&
                        (!request.CellId.HasValue || x.CellId == request.CellId) &&
                        (!request.SiteId.HasValue || x.Cell.SiteId == request.SiteId) &&
                        (!request.ClusterId.HasValue || x.Cell.Site.ClusterId == request.ClusterId) &&
                        (!request.DistrictId.HasValue || x.Cell.Site.Cluster.DistrictId == request.DistrictId) &&
                        (!request.CityId.HasValue || x.Cell.Site.Cluster.District.CityId == request.CityId) &&
                        (!request.StateRegionId.HasValue ||
                         x.Cell.Site.Cluster.District.City.StateRegionId == request.StateRegionId) &&
                        (!request.CountryId.HasValue || x.Cell.Site.Cluster.District.City.StateRegion.CountryId ==
                            request.CountryId);


                var npsResults = await _readRepository.GetAsNoTrackingAsync(expression,
                    x => x.Include(e => e.Cell)
                        .ThenInclude(e => e.Site)
                        .ThenInclude(e => e.Cluster)
                        .ThenInclude(e => e.District)
                        .ThenInclude(e => e.City)
                        .ThenInclude(e => e.StateRegion)
                        .ThenInclude(e => e.Country));


                var response = new ResponseResult<OverallNpsDto>()
                {
                    Entity = new OverallNpsDto()
                    {
                        CellId = npsResults?.CellId ?? Guid.Empty,
                        CellName = npsResults?.Cell?.CellName ?? "",
                        NpsValue = npsResults?.NpsValue ?? 0

                    }
                };

                return response;
            }
        }
        public class OverallNpsValidator : AbstractValidator<OverallNpsQuery>
        {
            public OverallNpsValidator()
            {
                RuleFor(x => x.Date).NotEmpty();
            }
        }
    }
}
