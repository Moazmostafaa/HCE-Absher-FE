using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.General
{
    public class Attachment : UpdateSoftDeleteEntity<Guid>
    {
        public Guid AttachmentId
        {
            get => Id;
            set => Id = value;
        }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Extention { get; set; }
        public long SizeByByte { get; set; }
        public int ModuleId { get; set; }
        public Modules Module { get; set; }
        public virtual User UserProfile { get; set; }
        public virtual User UserIdentification { get; set; }
    }
}
