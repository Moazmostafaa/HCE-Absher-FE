using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.AdminUser
{
    public class RoleDtoReq
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
