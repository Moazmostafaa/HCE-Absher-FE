using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.BackgroundService.NPS
{
    public interface INpsRecurringJob
    {
        void CalculateOverallNps();
    }
}
