using HCE.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Lookup
{
    public class SystemConfigration : UpdateSoftDeleteEntity<Guid>
    {
        public Guid SystemConfigrationId
        {
            get => Id;
            set => Id = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; } //int, string ...eg
        public bool IsActive { get; set; }
    }
}
