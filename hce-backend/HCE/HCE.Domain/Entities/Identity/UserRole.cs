using HCE.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Identity
{
    public class UserRole : UpdateSoftDeleteEntity<Guid>
    {
        public Guid UserRoleId
        {
            get => Id;
            set => Id = value;
        }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
