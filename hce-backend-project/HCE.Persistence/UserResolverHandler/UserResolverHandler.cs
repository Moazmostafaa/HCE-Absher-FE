using HCE.Interfaces.Models.Dto.User;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Persistence.Repositories.Blob;
using HCE.Utility.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Persistence.UserResolverHandler
{
    public class UserResolverHandler : IUserResolverHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        public UserResolverHandler(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.GetLoggedInUserName();// .Identity?.Name;
        }
        public string GetUserFullName()
        {
            return _httpContextAccessor.HttpContext?.User?.GetLoggedInUserFullName();
        }
        public Guid? GetUserProfileImageId()
        {
            var imageId = _httpContextAccessor.HttpContext?.User?.GetLoggedInProfileImage();
            var parsed = Guid.TryParse(imageId, out Guid id);
            if (parsed)
                return id;
            return null;
        }

        public UserDto GetUser()
        {
            var principal = _httpContextAccessor.HttpContext?.User;
            if (principal == null) return null;
            return new UserDto()
            {
                UserId = Guid.Parse(principal.FindFirstValue(ClaimTypes.Sid)),
                Email = principal.FindFirstValue(ClaimTypes.Email),
                FullName = principal.FindFirstValue(ClaimTypes.GivenName),
                PhoneNumber = principal.FindFirstValue(ClaimTypes.MobilePhone),
                UserName = principal.FindFirstValue(ClaimTypes.Name),
                ProfileImageId = Guid.Parse(principal.FindFirstValue("profile_image"))
            };
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.GetLoggedInUserId<string>();
        }

        public Guid GetUserGuid()
        {
            return Guid.Parse(GetUserId());
        }
    }
}
