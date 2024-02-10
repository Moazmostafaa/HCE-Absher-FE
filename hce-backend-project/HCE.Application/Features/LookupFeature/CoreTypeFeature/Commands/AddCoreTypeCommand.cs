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

namespace HCE.Application.Features.LookupFeature.CoreTypeFeature.Commands
{
    public class AddCoreTypeCommand : CommandBase<ResponseResult<CoreTypeDto>>
    {
        public string NPSKPIWeightName { get; set; }

        public string NPSKPIWeightDesc { get; set; }



        private class Handler : IRequestHandler<AddCoreTypeCommand, ResponseResult<CoreTypeDto>>
        {
            private readonly IWriteRepository<CoreType> _write;
            private readonly IReadRepository<CoreType> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<CoreType> read, IWriteRepository<CoreType> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<CoreTypeDto>> Handle(AddCoreTypeCommand request, CancellationToken cancellationToken)
            {
                var coreType = new CoreType
                {
                  
                    NPSKPIWeightName = request.NPSKPIWeightName,
                    NPSKPIWeightDesc = request.NPSKPIWeightDesc,
                    UserId = _userResolverHandler.GetUserGuid(),

                };

                await _write.AddAsync(coreType);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<CoreTypeDto>()
                {

                    Entity = new CoreTypeDto()
                    {
                        NPSKPIWeightId = coreType.Id,
                        NPSKPIWeightName = coreType.NPSKPIWeightName,
                        NPSKPIWeightDesc = coreType.NPSKPIWeightDesc,
                        CreationDate = coreType.CreatedDate,
                        CreatedBy = coreType.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddCoreTypeCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.NPSKPIWeightName).NotEmpty();


                }
            }

        }
    }
}

