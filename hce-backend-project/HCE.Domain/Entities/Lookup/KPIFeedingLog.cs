using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Customers;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.KPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Lookup
{
    public class KPIFeedingLog : UpdateSoftDeleteEntity<Guid>
    {
        public Guid KPIId { get; set; }
        public Kpi KPI { get; set; }
        public double Value { get; set; }
        public DateTime KPIFeedingLogDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid DataSourceId { get; set; }
        public DataSource DataSource { get; set; }
        public Guid? CellId { get; set; }
        public Cell Cell { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsCalculated { get; set; }
    }
}
