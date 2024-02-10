using HCE.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Identity
{
    public class RoleModules : UpdateSoftDeleteEntity<Guid>
    {
        public Guid RoleModulesId
        {
            get => Id;
            set => Id = value;
        }
        public Guid RoleId { get; set; }
        public int ModuleId { get; set; }
        public Role Role { get; set; }
        public Modules Module { get; set; }
    }
}
