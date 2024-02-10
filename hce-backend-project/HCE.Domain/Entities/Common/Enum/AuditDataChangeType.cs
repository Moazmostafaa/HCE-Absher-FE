using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Enum
{
    public enum AuditDataChangeType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3,
        SoftDelete = 4
    }
}
