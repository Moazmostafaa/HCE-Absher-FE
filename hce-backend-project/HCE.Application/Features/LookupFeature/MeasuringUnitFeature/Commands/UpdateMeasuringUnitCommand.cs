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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.LookupFeature.MeasuringUnitFeature.Commands
{
    public class UpdateMeasuringUnitCommand : CommandBase<ResponseResult<MeasuringUnitDto>>
    {
        public Guid MeasuringUnitId { get; set; }
        public string MeasuringUnitName { get; set; }

        public string MeasuringUnitDesc { get; set; }


        private class Handler : IRequestHandler<UpdateMeasuringUnitCommand, ResponseResult<MeasuringUnitDto>>
        {
            private readonly IWriteRepository<MeasuringUnit> _write;
            private readonly IReadRepository<MeasuringUnit> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<MeasuringUnit> read, IWriteRepository<MeasuringUnit> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<MeasuringUnitDto>> Handle(UpdateMeasuringUnitCommand request, CancellationToken cancellationToken)
            {
                var measuringUnit = await _read.GetAsync(x => x.Id == request.MeasuringUnitId);
                if (measuringUnit == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                measuringUnit.MeasuringUnitName = request.MeasuringUnitName;
                measuringUnit.MeasuringUnitDesc = request.MeasuringUnitDesc;
                measuringUnit.UpdatedBy = _userResolverHandler.GetUserId();
                measuringUnit.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                _write.Update(measuringUnit);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<MeasuringUnitDto>()
                {

                    Entity = new MeasuringUnitDto()
                    {
                        MeasuringUnitId = measuringUnit.Id,
                        MeasuringUnitName = measuringUnit.MeasuringUnitName,
                        MeasuringUnitDesc = measuringUnit.MeasuringUnitDesc,
                        CreationDate = measuringUnit.CreatedDate,
                        CreatedBy = measuringUnit.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateMeasuringUnitCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.MeasuringUnitId).NotEmpty();

                    RuleFor(x => x.MeasuringUnitName).NotEmpty();


                }
            }

        }
    }
}
