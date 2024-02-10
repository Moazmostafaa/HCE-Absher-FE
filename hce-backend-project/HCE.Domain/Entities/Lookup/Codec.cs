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
    public class Codec:UpdateSoftDeleteEntity<Guid>
    {
        public string CodecName { get; set; }

        public string CodecDesc { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Kpi> KPI { get; set; }

    }
}
