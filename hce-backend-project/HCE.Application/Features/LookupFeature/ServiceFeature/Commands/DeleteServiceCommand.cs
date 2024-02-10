using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
using HCE.Utility.Exceptions;
using HCE.Utility.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.LookupFeature.ServiceFeature.Commands
{
    public class DeleteServiceCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteServiceCommand, ResponseResult<bool>>
        {
            private readonly IWriteRepository<Service> _write;
            private readonly IReadRepository<Service> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Service> read, IWriteRepository<Service> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;

            }
            public async Task<ResponseResult<bool>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
            {
                var service = await _read.GetAsync(x => x.Id == request.Id);
                if (service == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                service.IsDeleted = true;
                service.DeletedDate = DateTime.Now.GetCurrentDateTime();
                service.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(service);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteServiceCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}