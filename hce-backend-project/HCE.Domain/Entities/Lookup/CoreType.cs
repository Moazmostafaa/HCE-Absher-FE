using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HCE.Domain.Entities.Lookup
{
    public class CoreType:  UpdateSoftDeleteEntity<Guid>
    {
        public string NPSKPIWeightName { get; set; }

        public string NPSKPIWeightDesc { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
