using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Lookup
{
    public class DataSource : UpdateSoftDeleteEntity<Guid>
    {
        public string DataSourceName { get; set; }

        public string DataSourceDesc { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<KPIFeedingLog> KPIFeedingLogs { get; set; }
    }
}
