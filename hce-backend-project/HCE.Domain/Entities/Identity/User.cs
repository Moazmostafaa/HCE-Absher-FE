using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Common.Enum;
using HCE.Domain.Entities.General;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Entities.KPIs;

namespace HCE.Domain.Entities.Identity
{
    public class User : UpdateSoftDeleteEntity<Guid>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public Guid? ProfileAttachmentId { get; set; }
        public Attachment ProfileAttachment { get; set; }
        public Guid? IdentificationAttachmentId { get; set; }
        public Attachment IdentificationAttachment { get; set; }
        public string NationalId { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        public int? BlockPeriod { get; set; }
        public Guid? BlockLawId { get; set; }
        public UserToken UserToken { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
