using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.KPI
{
    public class KPIDto
    {
        public Guid KPIID { get; set; }

        public string KPIName { get; set; }

        public string KPIAbbreviation { get; set; }

        public string KPIDesc { get; set; }
        public string KPIDefFormula { get; set; }

        public string KPITarget { get; set; }
        public string KPIEntitity { get; set; }

        public string GoalOptimization { get; set; }
        public int KPIBadThreshold { get; set; }

        public int KPIGoodThreshold { get; set; }
        public bool ISKPIExperienceCustomer { get; set; }
        public bool ISKPINPS { get; set; }

        public string KPIGenericFormula { get; set; }

        public string KPIVendorFormula { get; set; }

        public int KPIDefaultWeight { get; set; }

        public int KPICalculatedWeight { get; set; }

  

        public Guid KPICategoryId { get; set; }
        public string KPICategoryName { get; set; }

        public Guid DomainId { get; set; }
        public string DomainName { get; set; }

        public Guid SubSystemId { get; set; }
        public string SubSystemName { get; set; }

        public Guid PriorityId { get; set; }
        public string PriorityName { get; set; }

        public Guid MeasuringUnitId { get; set; }
        public string MeasuringUnitName { get; set; }

        public Guid CodecId { get; set; }
        public string CodecName { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }

        public Guid CellId { get; set; }
        public string CellName { get; set; }

        public Guid NPSKPIWeightId { get; set; }
        public string NPSKPIWeightName { get; set; }
    }
}
