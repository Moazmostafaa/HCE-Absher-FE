using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.KPIs;
using HCE.Domain.Entities.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.KPIs
{
    public class Weight : UpdateSoftDeleteEntity<Guid>
    {
        public double WeightValue { get; set; }
        public Guid? KpiId { get; set; }
        public Kpi Kpi { get; set; }
        public Guid? ServiceId { get; set; }
        public Service Service { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
