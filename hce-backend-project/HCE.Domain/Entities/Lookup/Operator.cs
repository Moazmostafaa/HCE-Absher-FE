using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Lookup
{
    public class Operator : UpdateSoftDeleteEntity<Guid>
    {
        public string OperatorName { get; set; }

        public string OperatorDesc { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid OperatorGroupId { get; set; }
        public OperatorGroup OperatorGroup { get; set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; }

    }
}
