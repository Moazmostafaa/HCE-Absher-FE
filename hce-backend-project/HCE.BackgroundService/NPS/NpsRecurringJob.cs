using Hangfire;
using HCE.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.BackgroundService.NPS
{
    public class NpsRecurringJob : INpsRecurringJob
    {
        private readonly INpsCalculationManager _calculationManager;
        private readonly IRecurringJobManager _recurringJobManager;
        public NpsRecurringJob(INpsCalculationManager calculationManager, IRecurringJobManager recurringJobManager)
        {
            _calculationManager = calculationManager;
            _recurringJobManager = recurringJobManager;
        }

        public void CalculateOverallNps()
        {
            _recurringJobManager.AddOrUpdate("CalculateOverallNps", () => _calculationManager.CalculateKpisNps(), Cron.Daily());
        }
    }
}
