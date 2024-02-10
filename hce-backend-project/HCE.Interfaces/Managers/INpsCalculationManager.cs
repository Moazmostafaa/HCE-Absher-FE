using HCE.Interfaces.Models.Dto.NPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Interfaces.Managers
{
    public interface INpsCalculationManager
    {
        Task CalculateKpisNps();
        decimal CalculateOverallNps(List<NpsCalculationRequestDto> feedingLogs);
    }
}
