using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HCE.Interfaces.Models.Dto.User.UserProfile;
using HCE.Domain.Entities.Identity;
using HCE.Interfaces.Enums;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Persistence.Repositories.Blob;
using HCE.Resource;
using HCE.Utility.Exceptions;
using HCE.Application.Common;
using HCE.Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HCE.Application.Features.GetProfileFeature.Queries
{
    public class GetProfileCommand : QueryBase<ResponseResult<UserProfileDto>>
    {
        private class Handler : IRequestHandler<GetProfileCommand, ResponseResult<UserProfileDto>>
        {
            private readonly IReadRepository<User> _userReadRepo;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadBlobRepository _blobRepository;

            public Handler(IReadRepository<User> userReadRepo, IUserResolverHandler userResolverHandler, IReadBlobRepository blobRepository)
            {
                _userReadRepo = userReadRepo;
                _userResolverHandler = userResolverHandler;
                _blobRepository = blobRepository;
            }

            public async Task<ResponseResult<UserProfileDto>> Handle(GetProfileCommand request, CancellationToken cancellationToken)
            {
                var user = await _userReadRepo.GetAsync(c => c.Id == _userResolverHandler.GetUserGuid(),
                    c => c.Include(x => x.IdentificationAttachment)
                        .Include(x => x.ProfileAttachment));

                if (user == null)
                    throw new BusinessException(Message_Resource.UserNotFound);

                var result = new ResponseResult<UserProfileDto>()
                {
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Entity = new UserProfileDto()
                    {
                        UserId = user.Id,
                        FullName = user.Name,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        Gender = (GenderEnum)user.Gender,
                        NationalId = user.NationalId,
                        IsActive = user.IsActive,
                        ProfileAttachmentId = user.ProfileAttachmentId,
                        IdentificationAttachmentId = user.IdentificationAttachmentId,
                        IdentificationAttachment = _blobRepository.GetAttachment(user.IdentificationAttachment),
                        ProfileAttachment = _blobRepository.GetAttachment(user.ProfileAttachment)
                    }
                };
                return result;
            }
        }
    }
}
