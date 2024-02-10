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
    public class Service : UpdateSoftDeleteEntity<Guid>
    {
        public string ServiceName { get; set; }
        public string ServiceDesc { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid? ParentServiceId { get; set; }
        public Service ParentService { get; set; }
        public ICollection<Kpi> Kpis { get; set; }
        public Weight Weight { get; set; }


    }
}
