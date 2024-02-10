using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Domain
{
    public interface IEntity<TId> : ICloneable, IEquatable<IEntity<TId>> where TId : struct
    {
        TId Id { get; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
