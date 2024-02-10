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

namespace HCE.Application.Features.LookupFeature.SubSystemFeature.Commands
{
    public class UpdateSubSystemCommand : CommandBase<ResponseResult<SubSystemDto>>
    {
        public Guid SubSystemId { get; set; }

        public string SubSystemName { get; set; }
        public string SubSystemDesc { get; set; }


        private class Handler : IRequestHandler<UpdateSubSystemCommand, ResponseResult<SubSystemDto>>
        {
            private readonly IWriteRepository<SubSystem> _write;
            private readonly IReadRepository<SubSystem> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<SubSystem> read, IWriteRepository<SubSystem> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<SubSystemDto>> Handle(UpdateSubSystemCommand request, CancellationToken cancellationToken)
            {
                var subSystem = await _read.GetAsync(x => x.Id == request.SubSystemId);
                if (subSystem == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                subSystem.SubSystemName = request.SubSystemName;
                subSystem.SubSystemDesc = request.SubSystemDesc;
                subSystem.UpdatedBy = _userResolverHandler.GetUserId();
                subSystem.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                _write.Update(subSystem);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<SubSystemDto>()
                {

                    Entity = new SubSystemDto()
                    {
                        SubSystemId = subSystem.Id,
                        SubSystemName = subSystem.SubSystemName,
                        SubSystemDesc = subSystem.SubSystemDesc,
                        CreationDate = subSystem.CreatedDate,
                        CreatedBy = subSystem.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateSubSystemCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.SubSystemId).NotEmpty();

                    RuleFor(x => x.SubSystemName).NotEmpty();


                }
            }

        }
    }
}
