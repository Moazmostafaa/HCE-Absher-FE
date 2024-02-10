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

namespace HCE.Application.Features.LookupFeature.DomainFeature.Commands
{
    public class UpdateDomainCommand : CommandBase<ResponseResult<DomainDto>>
    {
        public Guid DomainId { get; set; }

        public string DomainName { get; set; }

        public string DomainDesc { get; set; }


        private class Handler : IRequestHandler<UpdateDomainCommand, ResponseResult<DomainDto>>
        {
            private readonly IWriteRepository<Domains> _write;
            private readonly IReadRepository<Domains> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Domains> read, IWriteRepository<Domains> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<DomainDto>> Handle(UpdateDomainCommand request, CancellationToken cancellationToken)
            {
                var domain = await _read.GetAsync(x => x.Id == request.DomainId);
                if (domain == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                domain.DomainName = request.DomainName;
                domain.DomainDesc = request.DomainDesc;
                domain.UpdatedBy = _userResolverHandler.GetUserId();
                domain.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                _write.Update(domain);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<DomainDto>()
                {

                    Entity = new DomainDto()
                    {
                        DomainId = domain.Id,
                        DomainName = domain.DomainName,
                        DomainDesc = domain.DomainDesc,
                        CreationDate = domain.CreatedDate,
                        CreatedBy = domain.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateDomainCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.DomainId).NotEmpty();

                    RuleFor(x => x.DomainName).NotEmpty();


                }
            }

        }
    }
}

