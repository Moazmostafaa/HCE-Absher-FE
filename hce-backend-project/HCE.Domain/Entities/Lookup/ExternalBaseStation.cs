using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Lookup
{
    public class ExternalBaseStation : UpdateSoftDeleteEntity<Guid>
    {
        public string ExternalBaseStationName { get; set; }

        public string ExternalBaseStationDesc { get; set; }
        public string ExternalBaseStationBTSType { get; set; }
        public DateTime ExternalBaseStationRFSDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid SiteId { get; set; }
        public Site Site { get; set; }
    }
}
