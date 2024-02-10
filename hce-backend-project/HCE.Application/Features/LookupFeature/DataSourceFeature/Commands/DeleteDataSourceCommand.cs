using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Lookup;
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
namespace HCE.Application.Features.LookupFeature.DataSourceFeature.Commands
{
    public class DeleteDataSourceCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteDataSourceCommand, ResponseResult<bool>>
        {
            private readonly IWriteRepository<Goal> _write;
            private readonly IReadRepository<Goal> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Goal> read, IWriteRepository<Goal> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;

            }
            public async Task<ResponseResult<bool>> Handle(DeleteDataSourceCommand request, CancellationToken cancellationToken)
            {
                var dataSource = await _read.GetAsync(x => x.Id == request.Id);
                if (dataSource == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                dataSource.IsDeleted = true;
                dataSource.DeletedDate = DateTime.Now.GetCurrentDateTime();
                dataSource.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(dataSource);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteDataSourceCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}