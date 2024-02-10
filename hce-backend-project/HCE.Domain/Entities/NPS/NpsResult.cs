using HCE.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Entities.Lookup;

namespace HCE.Domain.Entities.NPS
{
    public class NpsResult : UpdateSoftDeleteEntity<Guid>
    {
        public decimal NpsValue { get; set; }
        public Guid CellId { get; set; }
        public Cell Cell { get; set; }
    }
}
