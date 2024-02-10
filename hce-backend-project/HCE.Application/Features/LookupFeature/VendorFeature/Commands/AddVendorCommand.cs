﻿using FluentValidation;
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

namespace HCE.Application.Features.LookupFeature.VendorFeature.Commands
{
    public class AddVendorCommand : CommandBase<ResponseResult<VendorDto>>
    {
        public string VendorName { get; set; }

        public string VendorDesc { get; set; }



        private class Handler : IRequestHandler<AddVendorCommand, ResponseResult<VendorDto>>
        {
            private readonly IWriteRepository<Vendor> _write;
            private readonly IReadRepository<Vendor> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Vendor> read, IWriteRepository<Vendor> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<VendorDto>> Handle(AddVendorCommand request, CancellationToken cancellationToken)
            {
                var vendor = new Vendor
                {
                    VendorName = request.VendorName,
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

