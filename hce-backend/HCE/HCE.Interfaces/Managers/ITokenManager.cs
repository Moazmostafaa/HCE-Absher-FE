using HCE.Interfaces.Models.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Managers
{
    public interface ITokenManager
    {
        string GenerateToken(Guid userId, string name, string phoneNumber, string userName, Guid? profileImageId);
        Task<bool> UpdateConnectionId(Guid userId, string ConnectionId);
        Task<string> GetConnectionId(Guid userId);
        Task<bool> CreateOrUpdateToken(Guid userId, string jwtToken);
    }
}
