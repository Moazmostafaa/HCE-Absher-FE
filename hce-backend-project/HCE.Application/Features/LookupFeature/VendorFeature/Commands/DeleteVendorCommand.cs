using System;
using System.Collections.Generic;
using System.Linq;
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
using HCE.Utility.Extensions;
using MediatR;

using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HCE.Utility.Exceptions;

namespace HCE.Application.Features.LookupFeature.VendorFeature.Commands
{
    public class DeleteVendorCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteVendorCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<Vendor> _read;
            private readonly IWriteRepository<Vendor> _write;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Vendor> read, IWriteRepository<Vendor> write, IUnitOfWork unitOfWork
               )
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;

            }
            public async Task<ResponseResult<bool>> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
            {
                var vendor = await _read.GetAsync(x => x.Id == request.Id);
                if (vendor == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                vendor.IsDeleted = true;
                vendor.DeletedDate = DateTime.Now.GetCurrentDateTime();
                vendor.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(vendor);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteVendorCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}
