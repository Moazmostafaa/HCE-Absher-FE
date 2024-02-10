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
    public class Cell : UpdateSoftDeleteEntity<Guid>
    {
        public string CellName { get; set; }
        public string CellDesc { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid AccessTechnologyId { get; set; }
        public AccessTechnology AccessTechnology { get; set; }
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public Guid GoalId { get; set; }
        public Goal Goal { get; set; }
        public Guid SiteId { get; set; }
        public Site Site { get; set; }
        public ICollection<KPIFeedingLog> KPIFeedingLogs { get; set; }


    }
}
