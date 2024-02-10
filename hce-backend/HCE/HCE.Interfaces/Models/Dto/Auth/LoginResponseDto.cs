using HCE.Interfaces.Models.Dto.User.UserAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Auth
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public string Grand { get; set; }
        public string Department { get; set; }
        public Guid? ProfileAttachmentId { get; set; }
    }
}
