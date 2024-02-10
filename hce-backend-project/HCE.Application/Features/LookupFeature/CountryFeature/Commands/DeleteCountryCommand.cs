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

namespace HCE.Application.Features.LookupFeature.CountryFeature.Commands
{
    public class DeleteCountryCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteCountryCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<Country> _read;
            private readonly IWriteRepository<Country> _write;

            private readonly IReadRepository<StateRegion> _StateRegionread;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Country> read, IWriteRepository<Country> write, IUnitOfWork unitOfWork
               , IReadRepository<StateRegion> StateRegionread)
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;
                _StateRegionread = StateRegionread;

            }
            public async Task<ResponseResult<bool>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
            {
                var country = await _read.GetAsync(x => x.Id == request.Id);
                if (country == null)
                    throw new BusinessException(Message_Resource.CountryEntity);

                var stateRegions =  _StateRegionread.GetManyAsNoTracking(x => x.CountryId == request.Id);
                if (stateRegions != null)
                    throw new BusinessException(Message_Resource.CantDeleteCountryHasStateRegions);

                country.IsDeleted = true;
                country.DeletedDate = DateTime.Now.GetCurrentDateTime();
                country.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(country);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteCountryCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}
