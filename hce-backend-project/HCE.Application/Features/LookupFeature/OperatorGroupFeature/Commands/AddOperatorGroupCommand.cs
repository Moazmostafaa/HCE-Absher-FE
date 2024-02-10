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
namespace HCE.Application.Features.LookupFeature.OperatorGroupFeature.Commands
{
    public class AddOperatorGroupCommand : CommandBase<ResponseResult<OperatorGroupDto>>
    {
        public string OperatorGroupName { get; set; }
        public string OperatorGroupDesc { get; set; }



        private class Handler : IRequestHandler<AddOperatorGroupCommand, ResponseResult<OperatorGroupDto>>
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

            public async Task<ResponseResult<OperatorGroupDto>> Handle(AddOperatorGroupCommand request, CancellationToken cancellationToken)
            {
                var operatorGroup = new OperatorGroup
                {
                    OperatorGroupName = request.OperatorGroupName,
                    OperatorGroupDesc = request.OperatorGroupDesc,
                    UserId = _userResolverHandler.GetUserGuid()

                };

                await _write.AddAsync(operatorGroup);

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


            public class Validator : AbstractValidator<AddOperatorGroupCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.OperatorGroupName).NotEmpty();


                }
            }

        }
    }
}