using HCE.Interfaces.Models.Dto.User.UserAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.User
{
    public class UserDto
    {

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? ProfileImageId { get; set; }
        public AttachmentDto ProfileImage { get; set; }
        public string Email { get; set; }
    }
}
