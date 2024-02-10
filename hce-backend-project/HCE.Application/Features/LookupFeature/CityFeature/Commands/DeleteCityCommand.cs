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

namespace HCE.Application.Features.LookupFeature.CityFeature.Commands
{
    public class DeleteCityCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteCityCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<City> _read;
            private readonly IWriteRepository<City> _write;
            private readonly IReadRepository<District> _Ditrictsread;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<City> read, IWriteRepository<City> write, IUnitOfWork unitOfWork
               , IReadRepository<District> Ditrictsread)
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;
                _Ditrictsread = Ditrictsread;
            }
            public async Task<ResponseResult<bool>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
            {
                var city = await _read.GetAsync(x => x.Id == request.Id);
                if (city == null)
                    throw new EntityNotFoundException(Message_Resource.CityEntity);


                var Districts = _Ditrictsread.GetManyAsNoTracking(x => x.CityId == request.Id);
                if (Districts != null)
                    throw new BusinessException(Message_Resource.CantDeleteCitiesHasDistricts);

                city.IsDeleted = true;
                city.DeletedDate = DateTime.Now.GetCurrentDateTime();
                city.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(city);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteCityCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}