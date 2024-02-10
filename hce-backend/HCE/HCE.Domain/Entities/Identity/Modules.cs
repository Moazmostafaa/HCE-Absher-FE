using HCE.Domain.Abstracts;
using HCE.Domain.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Identity
{
   public class Modules : UpdateSoftDeleteEntity<int>
    {
        public int ModuleId
        {
            get => Id;
            set => Id = value;
        }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public ICollection<RoleModules> RoleModules { get; set; }
        public ICollection<Attachment> Attachment { get; set; }
    }
}
