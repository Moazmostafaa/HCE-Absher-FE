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
namespace HCE.Application.Features.LookupFeature.PriorityFeature.Commands
{
    public class UpdatePriorityCommand : CommandBase<ResponseResult<PriorityDto>>
    {
        public Guid PriorityId { get; set; }
        public string PriorityName { get; set; }
        public string PriorityDesc { get; set; }


        private class Handler : IRequestHandler<UpdatePriorityCommand, ResponseResult<PriorityDto>>
        {
            private readonly IWriteRepository<Priority> _write;
            private readonly IReadRepository<Priority> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Priority> read, IWriteRepository<Priority> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<PriorityDto>> Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
            {
                var priority = await _read.GetAsync(x => x.Id == request.PriorityId);
                if (priority == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                priority.PriorityName = request.PriorityName;
                priority.PriorityDesc = request.PriorityDesc;
                priority.UpdatedBy = _userResolverHandler.GetUserId();
                priority.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                _write.Update(priority);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<PriorityDto>()
                {

                    Entity = new PriorityDto()
                    {
                        PriorityId = priority.Id,
                        PriorityName = priority.PriorityName,
                        PriorityDesc = priority.PriorityDesc,
                        CreationDate = priority.CreatedDate,
                        CreatedBy = priority.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdatePriorityCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.PriorityId).NotEmpty();

                    RuleFor(x => x.PriorityName).NotEmpty();


                }
            }

        }
    }
}