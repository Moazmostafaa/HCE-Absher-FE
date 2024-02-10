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
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.LookupFeature.ExternalOperatorFeature.Commands
{
    public class AddExternalOperatorCommand : CommandBase<ResponseResult<ExternalOperatorDto>>
    {
        public string ExternalOperatorName { get; set; }

        public string ExternalOperatorDesc { get; set; }


        private class Handler : IRequestHandler<AddExternalOperatorCommand, ResponseResult<ExternalOperatorDto>>
        {
            private readonly IWriteRepository<ExternalOperator> _write;
            private readonly IReadRepository<ExternalOperator> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<ExternalOperator> read, IWriteRepository<ExternalOperator> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<ExternalOperatorDto>> Handle(AddExternalOperatorCommand request, CancellationToken cancellationToken)
            {
               

                var externalOperator = new ExternalOperator
                {
                    ExternalOperatorName = request.ExternalOperatorName,
                    ExternalOperatorDesc = request.ExternalOperatorDesc,
                    UserId = _userResolverHandler.GetUserGuid()

                };

                await _write.AddAsync(externalOperator);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<ExternalOperatorDto>()
                {

                    Entity = new ExternalOperatorDto()
                    {
                        ExternalOperatorId = externalOperator.Id,
                        ExternalOperatorName = externalOperator.ExternalOperatorName,
                        ExternalOperatorDesc = externalOperator.ExternalOperatorDesc,
                        CreationDate = externalOperator.CreatedDate,

                        CreatedBy = externalOperator.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddExternalOperatorCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.ExternalOperatorName).NotEmpty();

                    RuleFor(x => x.ExternalOperatorDesc).NotEmpty();

                }
            }

        }
    }
}