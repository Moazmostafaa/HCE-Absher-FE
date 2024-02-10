using HCE.Interfaces.Models.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Managers
{
    public interface IUserManager
    {
        Task<UserDto> GetDtoById(Guid id);
        Task<UserDto> GetDtoByNationalId(string nationalId);
        Task<UserDto> GetDtoByUserNameOrNID(string userNameOrNID);
        Task<UserDto> GetDtoByUserNameAndPassword(string userName, string password);
    }
}