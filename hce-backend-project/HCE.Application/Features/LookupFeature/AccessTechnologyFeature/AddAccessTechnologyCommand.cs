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

namespace HCE.Application.Features.LookupFeature.AccessTechnologyFeature
{
    public class AddAccessTechnologyCommand : CommandBase<ResponseResult<AccessTechnologyDto>>
    {
        public string ServiceName { get; set; }

        public string ServiceDesc { get; set; }



        private class Handler : IRequestHandler<AddAccessTechnologyCommand, ResponseResult<AccessTechnologyDto>>
        {
            private readonly IWriteRepository<AccessTechnology> _write;
            private readonly IReadRepository<AccessTechnology> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<AccessTechnology> read, IWriteRepository<AccessTechnology> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<AccessTechnologyDto>> Handle(AddAccessTechnologyCommand request, CancellationToken cancellationToken)
            {
                var accessTechnology = new AccessTechnology
                {
                    ServiceName = request.ServiceName,
                    ServiceDesc = request.ServiceDesc,
                    UserId = _userResolverHandler.GetUserGuid(),

                };

                await _write.AddAsync(accessTechnology);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<AccessTechnologyDto>()
                {

                    Entity = new AccessTechnologyDto()
                    {
                        ServiceId = accessTechnology.Id,
                        ServiceName = accessTechnology.ServiceName,
                        ServiceDesc = accessTechnology.ServiceDesc,
                        CreationDate = accessTechnology.CreatedDate,
                        CreatedBy = accessTechnology.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddAccessTechnologyCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.ServiceName).NotEmpty();


                }
            }

        }
    }
}
