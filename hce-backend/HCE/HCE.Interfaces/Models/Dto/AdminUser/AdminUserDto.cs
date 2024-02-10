using HCE.Interfaces.Enums;
using HCE.Interfaces.Models.Dto.User.UserAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.AdminUser
{
    public class AdminUserDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderEnum Gender { get; set; }
        public Guid? ProfileAttachmentId { get; set; }
        public string NationalId { get; set; }
        public Guid? IdentificationAttachmentId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<RoleDtoReq> Roles { get; set; }
    }
}
