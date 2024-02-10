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

namespace HCE.Application.Features.LookupFeature.DomainFeature.Commands
{
    public class AddDomainCommand : CommandBase<ResponseResult<DomainDto>>
    {
        public string DomainName { get; set; }

        public string DomainDesc { get; set; }



        private class Handler : IRequestHandler<AddDomainCommand, ResponseResult<DomainDto>>
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

            public async Task<ResponseResult<DomainDto>> Handle(AddDomainCommand request, CancellationToken cancellationToken)
            {
                var domain = new Domains
                {
                    DomainName = request.DomainName,
                    DomainDesc = request.DomainDesc,
                    UserId = _userResolverHandler.GetUserGuid()

                };

                await _write.AddAsync(domain);

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


            public class Validator : AbstractValidator<AddDomainCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.DomainName).NotEmpty();


                }
            }

        }
    }
}
