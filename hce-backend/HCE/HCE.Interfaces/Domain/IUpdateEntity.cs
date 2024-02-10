using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Domain
{
    public interface IUpdateEntity<TId> : IEntity<TId> where TId : struct
    {
        string UpdatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
