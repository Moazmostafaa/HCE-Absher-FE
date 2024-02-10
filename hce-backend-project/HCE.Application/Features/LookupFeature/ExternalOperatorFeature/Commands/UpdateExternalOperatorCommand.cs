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

namespace HCE.Application.Features.LookupFeature.ExternalOperatorFeature.Commands
{
    public class UpdateExternalOperatorCommand : CommandBase<ResponseResult<ExternalOperatorDto>>
    {
        public Guid ExternalOperatorId { get; set; }
        public string ExternalOperatorName { get; set; }

        public string ExternalOperatorDesc { get; set; }


        private class Handler : IRequestHandler<UpdateExternalOperatorCommand, ResponseResult<ExternalOperatorDto>>
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

            public async Task<ResponseResult<ExternalOperatorDto>> Handle(UpdateExternalOperatorCommand request, CancellationToken cancellationToken)
            {
                var externalOperator = await _read.GetAsync(x => x.Id == request.ExternalOperatorId);
                if (externalOperator == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                externalOperator.ExternalOperatorName = request.ExternalOperatorName;
                externalOperator.ExternalOperatorDesc = request.ExternalOperatorDesc;
                externalOperator.UpdatedBy = _userResolverHandler.GetUserId();
                externalOperator.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                _write.Update(externalOperator);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<ExternalOperatorDto>()
                {

                    Entity = new ExternalOperatorDto()
                    {
            
                        ExternalOperatorId = externalOperator.Id,
                        ExternalOperatorName = externalOperator.ExternalOperatorName,
                        ExternalOperatorDesc = externalOperator.ExternalOperatorDesc,
                        CreationDate = externalOperator.CreatedDate,
                        CreatedBy = externalOperator.UserId,



                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateExternalOperatorCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.ExternalOperatorId).NotEmpty();

                    RuleFor(x => x.ExternalOperatorName).NotEmpty();


                }
            }

        }
    }
}