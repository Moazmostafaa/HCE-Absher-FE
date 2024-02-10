using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.KPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Lookup
{
    public class Priority : UpdateSoftDeleteEntity<Guid>
    {
        public string PriorityName { get; set; }

        public string PriorityDesc { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Kpi> KPI { get; set; }

    }
}
