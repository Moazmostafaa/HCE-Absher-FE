using HCE.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.NPS
{
    public class NpsCalculationRequestDto
    {
        public decimal BadThreshold { get; set; }
        public decimal GoodThreshold { get; set; }
        public decimal ServiceWeight { get; set; }
        public decimal SubServiceWeight { get; set; }
        public decimal KpiWeight { get; set; }
        public decimal FeedingLogValue { get; set; }
        public KpiUnitEnum KpiUnit { get; set; }
        public string KpiName { get; set; }
        public string SubServiceName { get; set; }
    }
}
