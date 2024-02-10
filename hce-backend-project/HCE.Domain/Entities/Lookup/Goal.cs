using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Lookup
{
    public class Goal : UpdateSoftDeleteEntity<Guid>
    {
        public string GoalName { get; set; }

        public string GoalDesc { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Cell> Cells { get; set; }


    }
}
