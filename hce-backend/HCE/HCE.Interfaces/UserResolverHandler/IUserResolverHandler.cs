using HCE.Interfaces.Models.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.UserResolverHandler
{
    public interface IUserResolverHandler
    {
        string GetUserId();
        Guid GetUserGuid();
        string GetUserName();
        string GetUserFullName();
        Guid? GetUserProfileImageId();
        UserDto GetUser();
    }
}
