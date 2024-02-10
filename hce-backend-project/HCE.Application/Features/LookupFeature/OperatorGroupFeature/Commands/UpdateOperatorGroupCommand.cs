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

namespace HCE.Application.Features.LookupFeature.OperatorGroupFeature.Commands
{
    public class UpdateOperatorGroupCommand : CommandBase<ResponseResult<OperatorGroupDto>>
    {
        public Guid OperatorGroupId { get; set; }
        public string OperatorGroupName { get; set; }
        public string OperatorGroupDesc { get; set; }


        private class Handler : IRequestHandler<UpdateOperatorGroupCommand, ResponseResult<OperatorGroupDto>>
        {
            private readonly IWriteRepository<OperatorGroup> _write;
            private readonly IReadRepository<OperatorGroup> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<OperatorGroup> read, IWriteRepository<OperatorGroup> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<OperatorGroupDto>> Handle(UpdateOperatorGroupCommand request, CancellationToken cancellationToken)
            {
                var operatorGroup = await _read.GetAsync(x => x.Id == request.OperatorGroupId);
                if (operatorGroup == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                operatorGroup.OperatorGroupName = request.OperatorGroupName;
                operatorGroup.OperatorGroupDesc = request.OperatorGroupDesc;
                operatorGroup.UpdatedBy = _userResolverHandler.GetUserId();
                operatorGroup.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                _write.Update(operatorGroup);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<OperatorGroupDto>()
                {

                    Entity = new OperatorGroupDto()
                    {
                        OperatorGroupId = operatorGroup.Id,
                        OperatorGroupName = operatorGroup.OperatorGroupName,
                        OperatorGroupDesc = operatorGroup.OperatorGroupDesc,
                        CreationDate = operatorGroup.CreatedDate,
                        CreatedBy = operatorGroup.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateOperatorGroupCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.OperatorGroupId).NotEmpty();

                    RuleFor(x => x.OperatorGroupName).NotEmpty();


                }
            }

        }
    }
}