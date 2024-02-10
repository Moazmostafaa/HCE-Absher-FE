using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
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
    public class AddMeasuringUnitCommand : CommandBase<ResponseResult<MeasuringUnitDto>>
    {
        public string MeasuringUnitName { get; set; }

        public string MeasuringUnitDesc { get; set; }



        private class Handler : IRequestHandler<AddMeasuringUnitCommand, ResponseResult<MeasuringUnitDto>>
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

            public async Task<ResponseResult<MeasuringUnitDto>> Handle(AddMeasuringUnitCommand request, CancellationToken cancellationToken)
            {
                var measuringUnit = new MeasuringUnit
                {
                    MeasuringUnitName = request.MeasuringUnitName,
                    MeasuringUnitDesc = request.MeasuringUnitDesc,
                    UserId = _userResolverHandler.GetUserGuid()

                };

                await _write.AddAsync(measuringUnit);

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


            public class Validator : AbstractValidator<AddMeasuringUnitCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.MeasuringUnitName).NotEmpty();


                }
            }

        }
    }
}