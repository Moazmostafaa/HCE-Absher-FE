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

namespace HCE.Application.Features.LookupFeature.DataSourceFeature.Commands
{
    public class UpdateDataSourceCommand : CommandBase<ResponseResult<DataSourceDto>>
    {
        public Guid DataSourceId { get; set; }
        public string DataSourceName { get; set; }

        public string DataSourceDesc { get; set; }



        private class Handler : IRequestHandler<UpdateDataSourceCommand, ResponseResult<DataSourceDto>>
        {
            private readonly IWriteRepository<DataSource> _write;
            private readonly IReadRepository<DataSource> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<DataSource> read, IWriteRepository<DataSource> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
            }

            public async Task<ResponseResult<DataSourceDto>> Handle(UpdateDataSourceCommand request, CancellationToken cancellationToken)
            {
                var dataSource = await _read.GetAsync(x => x.Id == request.DataSourceId);
                if (dataSource == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                dataSource.DataSourceName = request.DataSourceName;
                dataSource.DataSourceDesc = request.DataSourceDesc;
                dataSource.UpdatedBy = _userResolverHandler.GetUserId();
                dataSource.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                _write.Update(dataSource);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<DataSourceDto>()
                {

                    Entity = new DataSourceDto()
                    {
                        DataSourceId = dataSource.Id,
                        DataSourceName = dataSource.DataSourceName,
                        DataSourceDesc = dataSource.DataSourceDesc,
                        CreationDate = dataSource.CreatedDate,
                        CreatedBy = dataSource.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateDataSourceCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.DataSourceId).NotEmpty();

                    RuleFor(x => x.DataSourceName).NotEmpty();


                }
            }

        }
    }
}

