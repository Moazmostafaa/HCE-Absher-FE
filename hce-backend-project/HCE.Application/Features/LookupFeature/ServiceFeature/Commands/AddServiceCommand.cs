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

namespace HCE.Application.Features.LookupFeature.ServiceFeature.Commands
{
    public class AddServiceCommand : CommandBase<ResponseResult<ServiceDto>>
    {
        public string ServiceName { get; set; }
        public string ServiceDesc { get; set; }



        private class Handler : IRequestHandler<AddServiceCommand, ResponseResult<ServiceDto>>
        {
            private readonly IWriteRepository<Service> _write;
            private readonly IReadRepository<Service> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Service> read, IWriteRepository<Service> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<ServiceDto>> Handle(AddServiceCommand request, CancellationToken cancellationToken)
            {
                var service = new Service
                {
                    ServiceName = request.ServiceName,
                    ServiceDesc = request.ServiceDesc,
                    UserId = _userResolverHandler.GetUserGuid(),

                };

                await _write.AddAsync(service);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<ServiceDto>()
                {

                    Entity = new ServiceDto()
                    {
                        ServiceId = service.Id,
                        ServiceName = service.ServiceName,
                        ServiceDesc = service.ServiceDesc,
                        CreationDate = service.CreatedDate,
                        CreatedBy = service.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddServiceCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.ServiceName).NotEmpty();


                }
            }

        }
    }
}
