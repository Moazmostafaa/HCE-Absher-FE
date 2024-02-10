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
namespace HCE.Application.Features.LookupFeature.OperatorFeature.Commands
{
    public class AddOperatorCommand : CommandBase<ResponseResult<OperatorDto>>
    {
        public string OperatorName { get; set; }
        public string OperatorDesc { get; set; }

        public Guid OperatorGroupId { get; set; }

        public Guid CountryId { get; set; }

        private class Handler : IRequestHandler<AddOperatorCommand, ResponseResult<OperatorDto>>
        {
            private readonly IWriteRepository<Operator> _write;
            private readonly IReadRepository<Operator> _read;

            private readonly IReadRepository<OperatorGroup> _readOperatorGroup;
            private readonly IReadRepository<Country> _readCountry;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Operator> read, IWriteRepository<Operator> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository, IReadRepository<OperatorGroup> readOperatorGroup,
          IReadRepository<Country> readCountry)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
                _readOperatorGroup = readOperatorGroup;
                _readCountry = readCountry;
            }

            public async Task<ResponseResult<OperatorDto>> Handle(AddOperatorCommand request, CancellationToken cancellationToken)
            {
                var operatorGroup = await _readOperatorGroup.GetAsync(x => x.Id == request.OperatorGroupId);
                if (operatorGroup == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var Country = await _readCountry.GetAsync(x => x.Id == request.CountryId);
                if (Country == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var Operator = new Operator
                {
                    OperatorName = request.OperatorName,
                    OperatorDesc = request.OperatorDesc,
                    OperatorGroupId=request.OperatorGroupId,
                    CountryId=request.CountryId,
                    UserId = _userResolverHandler.GetUserGuid()

                };

                await _write.AddAsync(Operator);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<OperatorDto>()
                {

                    Entity = new OperatorDto()
                    {
                        OperatorGroupId = operatorGroup.Id,
                        OperatorGroupName = operatorGroup.OperatorGroupName,
                        OperatorName = Operator.OperatorName,
                        OperatorDesc=Operator.OperatorDesc,
                        CreationDate = Operator.CreatedDate,
                       
                        CreatedBy = Operator.UserId,
                        CountryId=Country.Id,
                        CountryName=Country.CountryNameAr
                        

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddOperatorCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.OperatorName).NotEmpty();

                    RuleFor(x => x.OperatorGroupId).NotEmpty();

                }
            }

        }
    }
}