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
namespace HCE.Application.Features.LookupFeature.WorldRegionFeature.Commands
{
    public class DeleteWorldRegionCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteWorldRegionCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<WorldRegion> _read;
            private readonly IWriteRepository<WorldRegion> _write;
            private readonly IReadRepository<Country> _readCountry;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<WorldRegion> read, IWriteRepository<WorldRegion> write, IUnitOfWork unitOfWork
                , IReadRepository<Country> readCountry
               )
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;
                _readCountry = readCountry;
            }
            public async Task<ResponseResult<bool>> Handle(DeleteWorldRegionCommand request, CancellationToken cancellationToken)
            {
                var region = await _read.GetAsync(x => x.Id == request.Id);
                if (region == null)
                    throw new EntityNotFoundException(Message_Resource.WorldRegionEntity);

                var Countries = _readCountry.GetManyAsNoTracking(x => x.WordRegionId == request.Id);
                if (Countries != null)
                    throw new BusinessException(Message_Resource.CantDeleteWorldRegionHasCountries);

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


            public class Validator : AbstractValidator<DeleteWorldRegionCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}
