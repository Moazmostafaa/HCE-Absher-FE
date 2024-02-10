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
namespace HCE.Application.Features.LookupFeature.WeightFeature.Commands
{
    public class AddWeightCommand : CommandBase<ResponseResult<bool>>
    {
        public string WeightName { get; set; }

        public string WeightDesc { get; set; }
        public Guid OperatorId { get; set; }
        public Guid CountryId { get; set; }



        private class Handler : IRequestHandler<AddWeightCommand, ResponseResult<bool>>
        {
            private readonly IWriteRepository<Weights> _write;
            private readonly IReadRepository<Weights> _read;

            private readonly IReadRepository<Country> _readCountry;

            private readonly IReadRepository<Operator> _readOperator;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Weights> read, IWriteRepository<Weights> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository
                , IReadRepository<Country> readCountry,
                IReadRepository<Operator> readOperator)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _readCountry = readCountry;
                _readOperator = readOperator;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<bool>> Handle(AddWeightCommand request, CancellationToken cancellationToken)
            {
                var Operator = await _readOperator.GetAsync(x => x.Id == request.OperatorId);
                if (Operator == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var operatorGroup = await _readOperatorGroup.GetAsync(x => x.Id == request.OperatorGroupId);
                if (operatorGroup == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var weight = new Weights
                {
                    WeightName = request.,
                    VendorDesc = request.VendorDesc,
                    UserId = _userResolverHandler.GetUserGuid(),

                };

                await _write.AddAsync(vendor);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<VendorDto>()
                {

                    Entity = new VendorDto()
                    {
                        VendorId = vendor.Id,
                        VendorName = vendor.VendorName,
                        VendorDesc = vendor.VendorDesc,
                        CreationDate = vendor.CreatedDate,
                        CreatedBy = vendor.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddVendorCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.VendorName).NotEmpty();


                }
            }

        }
    }
}

