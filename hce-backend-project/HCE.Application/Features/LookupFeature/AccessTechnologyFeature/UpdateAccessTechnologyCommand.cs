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
namespace HCE.Application.Features.LookupFeature.AccessTechnologyFeature
{
    public class UpdateAccessTechnologyCommand : CommandBase<ResponseResult<AccessTechnologyDto>>
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }

        public string ServiceDesc { get; set; }



        private class Handler : IRequestHandler<UpdateAccessTechnologyCommand, ResponseResult<AccessTechnologyDto>>
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

            public async Task<ResponseResult<AccessTechnologyDto>> Handle(UpdateAccessTechnologyCommand request, CancellationToken cancellationToken)
            {
                var accessTechnology = await _read.GetAsync(x => x.Id == request.ServiceId);
                if (accessTechnology == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                accessTechnology.ServiceName = request.ServiceName;
                accessTechnology.ServiceDesc = request.ServiceDesc;
                accessTechnology.UpdatedBy = _userResolverHandler.GetUserId();
                accessTechnology.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                 _write.Update(accessTechnology);

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


            public class Validator : AbstractValidator<UpdateAccessTechnologyCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.ServiceId).NotEmpty();

                    RuleFor(x => x.ServiceName).NotEmpty();


                }
            }

        }
    }
}