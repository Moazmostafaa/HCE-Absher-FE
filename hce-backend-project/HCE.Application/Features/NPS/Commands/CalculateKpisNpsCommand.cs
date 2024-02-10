using HCE.Domain.Entities.KPIs;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.NPS;
using HCE.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HCE.Domain.Entities.NPS;
using HCE.Interfaces.Enums;
using HCE.Interfaces.Managers;
using HCE.Utility.Extensions;
using HCE.Resource;
using System.Net;

namespace HCE.Application.Features.NPS.Commands
{
    public class CalculateKpisNpsCommand : IRequest<ResponseResult<List<NpsCalculationDto>>>
    {
        public class Handler : IRequestHandler<CalculateKpisNpsCommand, ResponseResult<List<NpsCalculationDto>>>
        {
            private readonly IReadRepository<Kpi> _kpiReadRepository;
            private readonly IReadRepository<KPIFeedingLog> _feedingLogReadRepository;
            private readonly IWriteRepository<KPIFeedingLog> _feedingLogWriteRepository;
            private readonly IWriteRepository<NpsResult> _npsWriteRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly INpsCalculationManager _calculationManager;
            public Handler(IReadRepository<Kpi> kpiRepository,
                IReadRepository<KPIFeedingLog> feedingLogReadRepository,
                IWriteRepository<KPIFeedingLog> feedingLogWriteRepository,
                IUnitOfWork unitOfWork,
                INpsCalculationManager calculationManager, IWriteRepository<NpsResult> npsWriteRepository)
            {
                _kpiReadRepository = kpiRepository;
                _feedingLogReadRepository = feedingLogReadRepository;
                _feedingLogWriteRepository = feedingLogWriteRepository;
                _unitOfWork = unitOfWork;
                _calculationManager = calculationManager;
                _npsWriteRepository = npsWriteRepository;
            }
            public async Task<ResponseResult<List<NpsCalculationDto>>> Handle(CalculateKpisNpsCommand request, CancellationToken cancellationToken)
            {
                var result = new List<NpsCalculationDto>();

                var notCalculatedFeedingLogs =
                   await _feedingLogReadRepository.GetMany(x => !x.IsCalculated,
                                                           e => e.Include(c => c.KPI)
                                                               .ThenInclude(c => c.Weight)
                                                               .Include(c => c.KPI)
                                                               .ThenInclude(c => c.MeasuringUnit)
                                                               .Include(c => c.KPI)
                                                               .ThenInclude(c => c.Service)
                                                               .Include(c => c.KPI)
                                                               .ThenInclude(c => c.Service)
                                                               .ThenInclude(c => c.Weight)
                                                               .Include(c => c.KPI)
                                                               .ThenInclude(c => c.Service)
                                                               .ThenInclude(c => c.ParentService)
                                                               .ThenInclude(c => c.Weight)).ToListAsync(cancellationToken: cancellationToken);


                if (notCalculatedFeedingLogs.Any())
                {
                    try
                    {
                        foreach (var feedingLogGroup in notCalculatedFeedingLogs.GroupBy(x => x.CellId))
                        {
                            var npsResult = CalculateNpsForCell(feedingLogGroup);

                            await _npsWriteRepository.AddAsync(new NpsResult()
                            {
                                CellId = feedingLogGroup.Key.Value,
                                CreatedDate = DateTime.Now.GetCurrentDateTime(),
                                NpsValue = npsResult,
                                CreatedBy = "System",
                            });

                            result.Add(new NpsCalculationDto()
                            {
                                CellId = feedingLogGroup.Key.Value,
                                Nps = npsResult
                            });
                        }

                        notCalculatedFeedingLogs.ForEach(x => x.IsCalculated = true);
                        await _feedingLogWriteRepository.BulkUpdateAsync(notCalculatedFeedingLogs);

                        await _unitOfWork.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await _unitOfWork.CommitAsync();
                        throw ex;
                    }
                }


                return new ResponseResult<List<NpsCalculationDto>>()
                {
                    Entity = result,
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };
            }

            private decimal CalculateNpsForCell(IGrouping<Guid?, KPIFeedingLog> feedingLogGroup)
            {
                return _calculationManager.CalculateOverallNps(
                    feedingLogGroup.ToList().Select(x => new NpsCalculationRequestDto()
                    {
                        KpiName = x.KPI.Name,
                        SubServiceName = x.KPI.Service.ServiceName,
                        BadThreshold = (decimal)x.KPI.BadThreshold,
                        GoodThreshold = (decimal)x.KPI.GoodThreshold,
                        KpiWeight = (decimal)x.KPI.Weight.WeightValue,
                        SubServiceWeight = (decimal)x.KPI.Service.Weight.WeightValue,
                        FeedingLogValue = (decimal)x.Value,
                        ServiceWeight = (decimal)x.KPI.Service.ParentService.Weight.WeightValue,
                        //KpiUnit = x.KPI.MeasuringUnit.MeasuringUnitName == "%" ? KpiUnitEnum.Percent : KpiUnitEnum.Num
                    }).ToList());
            }
        }
    }
}
