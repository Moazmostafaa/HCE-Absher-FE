using HCE.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Abstracts
{
    public class SoftDeleteEntity<TId> : EntityBase<TId>, ISoftDeletable where TId : struct
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
