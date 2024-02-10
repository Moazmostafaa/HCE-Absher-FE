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
    public class KpiCategories : UpdateSoftDeleteEntity<Guid>
    {
        public Guid KpiId { get; set; }
        public Kpi Kpi { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
