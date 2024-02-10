using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Common.Enum;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Enums;
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
namespace HCE.Application.Features.LookupFeature.RegionFeature.Commands
{
    public class DeleteRegionCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteRegionCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<StateRegion> _read;
            private readonly IWriteRepository<StateRegion> _write;
            private readonly IReadRepository<City> _Cityread;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<StateRegion> read, IWriteRepository<StateRegion> write, IUnitOfWork unitOfWork
               , IReadRepository<City> Cityread)
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;
                _Cityread = Cityread;
 
            }
            public async Task<ResponseResult<bool>> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
            {
                var region = await _read.GetAsync(x => x.Id == request.Id);
                if (region == null)
                    throw new EntityNotFoundException(Message_Resource.StateRegionEntity);


                var Cities = _Cityread.GetManyAsNoTracking(x => x.StateRegionId == request.Id);
                if (Cities != null)
                    throw new BusinessException(Message_Resource.CantDeleteStateRegionsHasCities);

                region.IsDeleted = true;
                region.DeletedDate = DateTime.Now.GetCurrentDateTime();
                region.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(region);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteRegionCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}

