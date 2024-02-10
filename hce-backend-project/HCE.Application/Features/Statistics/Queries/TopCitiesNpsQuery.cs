using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Models.Dto.Statistics;
using HCE.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HCE.Application.Features.Statistics.Queries
{
    public class TopCitiesNpsQuery : IRequest<ResponseResult<List<TopCityNpsDto>>>
    {
        public DateTime Date { get; set; }

        internal class Handler : IRequestHandler<TopCitiesNpsQuery, ResponseResult<List<TopCityNpsDto>>>
        {
            private readonly IReadRepository<City> _cityRepository;

            public Handler(IReadRepository<City> cityRepository)
            {
                _cityRepository = cityRepository;
            }

            public async Task<ResponseResult<List<TopCityNpsDto>>> Handle(TopCitiesNpsQuery request, CancellationToken cancellationToken)
            {
                var result = await _cityRepository.GetManyAsNoTracking(x => x.IsTop).Include(x => x.StateRegion).Select(city => new TopCityNpsDto()
                {
                    CityNps = new Random().Next(0, 1000),
                    City = new CityDto()
                    {
                        CityId = city.Id,
                        CityNameEn = city.CityNameEn,
                        CityNameAr = city.CityNameAr,
                        CityDesc = city.CityDesc,
                        CityNameLang = city.CityNameLang,
                        StateRegionId = city.StateRegionId,
                        StateRegionNameEn = city.StateRegion.StateRegionNameEn,
                        StateRegionNameAr = city.StateRegion.StateRegionNameAr,
                        StateRegionNameLang = city.StateRegion.StateRegionNameLang,
                        IsTop = city.IsTop,
                        CreatedBy = city.UserId,
                        CreationDate = city.CreatedDate
                    },

                }).ToListAsync(cancellationToken);
                return new ResponseResult<List<TopCityNpsDto>>(result);
            }
        }
        public class Validator : AbstractValidator<TopCitiesNpsQuery>
        {
            public Validator()
            {
                RuleFor(x => x.Date).NotEmpty();
            }
        }
    }
}
