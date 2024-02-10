using HCE.Domain.Entities.Identity;
using HCE.Interfaces.Managers;
using HCE.Interfaces.Repositories;
using HCE.Resource;
using HCE.Utility.CommonModels;
using HCE.Utility.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HCE.Utility.Extensions;
using Serilog;

namespace HCE.Application.Managers
{
    public class TokenManager : ITokenManager
    {
        private readonly AppJWTSetting _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadRepository<UserToken> _tokenReadRepository;
        private readonly IWriteRepository<UserToken> _tokenWriteRepository;
        public TokenManager(IOptions<AppJWTSetting> appSettings, IUnitOfWork unitOfWork, IReadRepository<UserToken> readRepository, IWriteRepository<UserToken> tokenWriteRepository)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _tokenReadRepository = readRepository;
            _tokenWriteRepository = tokenWriteRepository;
        }

        public string GenerateToken(Guid userId, string name, string phoneNumber, string userName, Guid? profileImageId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Sid, userId.ToString()),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.GivenName, name),
                        new Claim(ClaimTypes.MobilePhone, phoneNumber ?? string.Empty), // PhoneNumber can't be null
                        new Claim(ClaimTypes.Name, userName),
                        new Claim("profile_image", profileImageId.HasValue? profileImageId.ToString():string.Empty),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAt,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GetConnectionId(Guid userId)
        {
            var token = await _tokenReadRepository.GetSingleAsync(e => e.UserId == userId);

            if (token == null)
                throw new NotFoundException(Message_Resource.UserNotFound);

            return token.ConnectionId;
        }

        public async Task<bool> UpdateConnectionId(Guid userId, string ConnectionId)
        {
            var token = _tokenReadRepository.GetSingle(e => e.UserId == userId);

            if (token == null)
                throw new NotFoundException(Message_Resource.UserNotFound);

            token.ConnectionId = ConnectionId;

            _tokenWriteRepository.Update(token);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> CreateOrUpdateToken(Guid userId, string jwtToken)
        {
            try
            {
                var token = _tokenReadRepository.GetSingle(e => e.UserId == userId);

                if (token == null)
                {
                    // Add 
                    token = new UserToken()
                    {
                        UserId = userId,
                        Token = jwtToken,
                        LastLoginDate = DateTime.Now.GetCurrentDateTime(),
                    };
                    await _tokenWriteRepository.AddAsync(token);
                }
                else
                {
                    // edit 
                    token.Token = jwtToken;
                    _tokenWriteRepository.Update(token);
                }
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
        }
    }
}
