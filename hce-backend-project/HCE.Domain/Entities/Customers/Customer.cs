using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;

namespace HCE.Domain.Entities.Customers
{
    public class Customer : UpdateSoftDeleteEntity<Guid>
    {
        public string CustomerName { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<KPIFeedingLog> KPIFeedingLogs { get; set; }

    }
}
