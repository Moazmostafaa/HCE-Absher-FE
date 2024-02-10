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


namespace HCE.Application.Features.LookupFeature.DistrictFeature.Commands
{
    public class DeleteDistrictCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteDistrictCommand, ResponseResult<bool>>
        {
            private readonly IReadRepository<District> _read;
            private readonly IWriteRepository<District> _write;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<Cluster> _Clusterread;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<District> read, IWriteRepository<District> write, IUnitOfWork unitOfWork
               , IReadRepository<Cluster> Clusterread)
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;
                _Clusterread = Clusterread;
            }
            public async Task<ResponseResult<bool>> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
            {
                var district = await _read.GetAsync(x => x.Id == request.Id);
                if (district == null)
                    throw new EntityNotFoundException(Message_Resource.DistrictEntity);



                var Clusters = _Clusterread.GetManyAsNoTracking(x => x.DistrictId == request.Id);
                if (Clusters != null)
                    throw new BusinessException(Message_Resource.CantDeleteCitiesHasDistricts);
                district.IsDeleted = true;
                district.DeletedDate = DateTime.Now.GetCurrentDateTime();
                district.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(district);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteDistrictCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}