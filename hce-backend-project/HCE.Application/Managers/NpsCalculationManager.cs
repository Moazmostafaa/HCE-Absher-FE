using HCE.Interfaces.Managers;
using HCE.Interfaces.Models.Dto.NPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Interfaces.Enums;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Entities.NPS;
using HCE.Interfaces.Repositories;
using HCE.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HCE.Application.Managers
{
    public class NpsCalculationManager : INpsCalculationManager
    {
        private readonly IReadRepository<KPIFeedingLog> _feedingLogReadRepository;
        private readonly IWriteRepository<KPIFeedingLog> _feedingLogWriteRepository;
        private readonly IWriteRepository<NpsResult> _npsWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NpsCalculationManager(IReadRepository<KPIFeedingLog> feedingLogReadRepository,
            IWriteRepository<KPIFeedingLog> feedingLogWriteRepository,
            IUnitOfWork unitOfWork, IWriteRepository<NpsResult> npsWriteRepository)
        {
            _feedingLogReadRepository = feedingLogReadRepository;
            _feedingLogWriteRepository = feedingLogWriteRepository;
            _unitOfWork = unitOfWork;
            _npsWriteRepository = npsWriteRepository;
        }
        public async Task CalculateKpisNps()
        {
            var currentDate = DateTime.Now.Date;
            var notCalculatedFeedingLogs =
                   await _feedingLogReadRepository.GetMany(x => !x.IsCalculated && x.KPIFeedingLogDate.Date == currentDate,
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
                                                               .ThenInclude(c => c.Weight)).ToListAsync();


            if (notCalculatedFeedingLogs.Any())
            {
                try
                {
                    foreach (var feedingLogGroup in notCalculatedFeedingLogs.GroupBy(x => x.CellId))
                    {
                        var npsResult = CalculateOverallNps(
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

                        await _npsWriteRepository.AddAsync(new NpsResult()
                        {
                            CellId = feedingLogGroup.Key.Value,
                            CreatedDate = DateTime.Now.GetCurrentDateTime(),
                            NpsValue = npsResult,
                            CreatedBy = "System",
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

        }

        public decimal CalculateOverallNps(List<NpsCalculationRequestDto> feedingLogs)
            => feedingLogs.Sum(item => CalculateNpsPoints(CalculateNpsPercent(CalculateNorm(item), item)));


        /// <summary>
        /// calculate form based on calculation  MIN(MAX(((H4-B4)/(C4-B4))*100,0),100)
        /// where H4 is feeding log value
        /// B4 is bad threshold
        /// c4 is good threshold
        /// if result is greater than 100 it will be 100
        /// if result is smaller than 0 it will be 0
        /// </summary>
        /// <param name="feedItem"></param>
        /// <returns>Nps norm</returns>
        private decimal CalculateNorm(NpsCalculationRequestDto feedItem)
        {

            var normResult = ((feedItem.FeedingLogValue - feedItem.BadThreshold) /
                              (feedItem.GoodThreshold - feedItem.BadThreshold)) * 100;

            return CalculateMinMax(normResult);
        }

        private decimal NormUnitValue(KpiUnitEnum kpiUnit)
        {
            // This is to determine weather to calculate as percent or not
            return kpiUnit switch
            {
                KpiUnitEnum.Num => 1,
                KpiUnitEnum.Percent => 1 / 100,
                _ => 1
            };
        }

        /// <summary>
        /// calculate npc percent value based on calculation =I4*F4*E4*D4 where
        /// I4 is norm value
        /// F4 is service weight
        /// E4 is sub-service weight
        /// D4 is kpi weight
        /// </summary>
        /// <param name="normResult"></param>
        /// <param name="feedItem"></param>
        /// <returns>NPS percent</returns>
        private decimal CalculateNpsPercent(decimal normResult, NpsCalculationRequestDto feedItem)
            => normResult * (feedItem.KpiWeight / 100) * (feedItem.SubServiceWeight / 100) * (feedItem.ServiceWeight / 100);

        /// <summary>
        /// calculate nps points based on calculation =J*10 where
        /// J is NPS percent value
        /// </summary>
        /// <param name="npsPercent"></param>
        /// <returns>NPS points</returns>
        private decimal CalculateNpsPoints(decimal npsPercent)
            => npsPercent * 10;

        /// <summary>
        /// if result is greater than 100 it will be 100
        /// if result is smaller than 0 it will be 0
        /// </summary>
        /// <param name="result"></param>
        /// <returns>decimal</returns>
        private decimal CalculateMinMax(decimal result)
            => result > 100 ? 100 : result < 0 ? 0 : result;

    }
}
