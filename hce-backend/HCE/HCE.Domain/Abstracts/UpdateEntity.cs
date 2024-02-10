using HCE.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Abstracts
{
    public class UpdateEntity<TId> : EntityBase<TId>, IUpdateEntity<TId> where TId : struct
    {
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
