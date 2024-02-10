using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Lookup
{
    public class OperatorGroup : UpdateSoftDeleteEntity<Guid>
    {
        public string OperatorGroupName { get; set; }

        public string OperatorGroupDesc { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Operator> Operators { get; set; }


    }
}
