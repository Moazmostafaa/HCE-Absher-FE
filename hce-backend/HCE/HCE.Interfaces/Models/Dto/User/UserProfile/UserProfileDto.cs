using HCE.Interfaces.Enums;
using HCE.Interfaces.Models.Dto.User.UserAttachment;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.User.UserProfile
{
    public class UserProfileDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public GenderEnum Gender { get; set; }
        public Guid? ProfileAttachmentId { get; set; }
        public AttachmentDto ProfileAttachment { get; set; }
        public Guid? IdentificationAttachmentId { get; set; }
        public AttachmentDto IdentificationAttachment { get; set; }
        public string NationalId { get; set; }
        public bool IsActive { get; set; }
        public string Department { get; set; }
        public string Grand { get; set; }
        public string Education { get; set; }
        public string JoinDate { get; set; }
        public string DateOfRegistration { get; set; }
        public string DateofRetirement { get; set; }
        public string MoiCardExpiryDate { get; set; }
        public string NoOfCommittees { get; set; }
        public string NoOfTrainings { get; set; }
        public string NoOfParticipation { get; set; }
        public string NicCardExpiryDate { get; set; }
        public string DrivingLicenseExpiryDate { get; set; }
    }
}


