using HCE.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Identity
{
    public class Role : UpdateSoftDeleteEntity<Guid>
    {
        public Guid RoleId
        {
            get => Id;
            set => Id = value;
        }
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleModules> RoleModules { get; set; }

    }
}
