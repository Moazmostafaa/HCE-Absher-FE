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

namespace HCE.Application.Features.LookupFeature.OperatorFeature.Commands
{
    public class UpdateOperatorCommand : CommandBase<ResponseResult<OperatorDto>>
    {
        public Guid OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string OperatorDesc { get; set; }

        public Guid OperatorGroupId { get; set; }


        private class Handler : IRequestHandler<UpdateOperatorCommand, ResponseResult<OperatorDto>>
        {
            private readonly IWriteRepository<Operator> _write;
            private readonly IReadRepository<Operator> _read;

            private readonly IReadRepository<OperatorGroup> _readOperatorGroup;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Operator> read, IWriteRepository<Operator> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository, IReadRepository<OperatorGroup> readOperatorGroup)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
                _readOperatorGroup = readOperatorGroup;
            }

            public async Task<ResponseResult<OperatorDto>> Handle(UpdateOperatorCommand request, CancellationToken cancellationToken)
            {
                var Operator = await _read.GetAsync(x => x.Id == request.OperatorId);
                if (Operator == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var operatorGroup = await _readOperatorGroup.GetAsync(x => x.Id == request.OperatorGroupId);
                if (operatorGroup == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                Operator.OperatorName = request.OperatorName;
                Operator.OperatorDesc = request.OperatorDesc;
                Operator.OperatorGroupId = request.OperatorGroupId;
                Operator.UpdatedBy = _userResolverHandler.GetUserId();
                Operator.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                _write.Update(Operator);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<OperatorDto>()
                {

                    Entity = new OperatorDto()
                    {
                        OperatorGroupId = operatorGroup.Id,
                        OperatorGroupName = operatorGroup.OperatorGroupName,
                        OperatorId = Operator.Id,
                        OperatorName=Operator.OperatorName,
                        OperatorDesc=Operator.OperatorDesc,
                        CreationDate = Operator.CreatedDate,
                        CreatedBy = Operator.UserId,



                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateOperatorCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.OperatorGroupId).NotEmpty();

                    RuleFor(x => x.OperatorName).NotEmpty();

                    RuleFor(x => x.OperatorId).NotEmpty();

                }
            }

        }
    }
}