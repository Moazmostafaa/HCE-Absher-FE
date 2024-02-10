using HCE.Application.Common;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
using HCE.Utility.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.EditProfile.Queries
{
    public class EditProfileCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid? ProfileAttachmentId { get; set; }
        private class Handler : IRequestHandler<EditProfileCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<User> _userReadRepository;
            private readonly IWriteRepository<User> _userWriteRepository;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IUnitOfWork _unitOfWork;
            public Handler(IReadRepository<User> userReadRepository, IUserResolverHandler userResolverHandler, IUnitOfWork unitOfWork, IWriteRepository<User> userWriteRepository)
            {
                _userReadRepository = userReadRepository;
                _userWriteRepository = userWriteRepository;
                _userResolverHandler = userResolverHandler;
                _unitOfWork = unitOfWork;

            }
            public async Task<ResponseResult<bool>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
            {
                var user = await _userReadRepository.GetAsync(x => x.Id == _userResolverHandler.GetUserGuid());
                if (user == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                user.ProfileAttachmentId = request.ProfileAttachmentId;

                _userWriteRepository.Update(user);

                bool result = (await _unitOfWork.CommitAsync()) > 0;
                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);

            }
        }
        public class Validator : AbstractValidator<EditProfileCommand>
        {
            public Validator()
            {
                RuleFor(x => x.ProfileAttachmentId).NotEmpty();
            }
        }
    }
}
