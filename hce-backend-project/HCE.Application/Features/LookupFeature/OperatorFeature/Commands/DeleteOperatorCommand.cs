﻿using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
using HCE.Utility.Exceptions;
using HCE.Utility.Extensions;
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
    public class DeleteOperatorCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<DeleteOperatorCommand, ResponseResult<bool>>
        {
            private readonly IWriteRepository<Operator> _write;
            private readonly IReadRepository<Operator> _read;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Operator> read, IWriteRepository<Operator> write, IUnitOfWork unitOfWork
               )
            {
                _userResolverHandler = userResolverHandler;

                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;

            }
            public async Task<ResponseResult<bool>> Handle(DeleteOperatorCommand request, CancellationToken cancellationToken)
            {
                var operatorGroup = await _read.GetAsync(x => x.Id == request.Id);
                if (operatorGroup == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                operatorGroup.IsDeleted = true;
                operatorGroup.DeletedDate = DateTime.Now.GetCurrentDateTime();
                operatorGroup.UpdatedBy = _userResolverHandler.GetUserId();
                _write.Update(operatorGroup);

                bool result = (await _unitOfWork.CommitAsync()) > 0;

                if (result)
                    return new ResponseResult<bool>(true);
                else
                    throw new SaveFailureException(Message_Resource.SaveField);
            }


            public class Validator : AbstractValidator<DeleteOperatorCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }
    }
}