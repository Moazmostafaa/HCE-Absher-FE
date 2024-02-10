using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HCE.Domain.Entities.Lookup
{
    public class ExternalOperator : UpdateSoftDeleteEntity<Guid>
    {
        public string ExternalOperatorName { get; set; }

        public string ExternalOperatorDesc { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
