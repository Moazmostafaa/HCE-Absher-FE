using FluentValidation;
using HCE.Application.Common;
using HCE.Application.Common.Validators;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Domain.Request;
using HCE.Interfaces.Models.Dto.Lookup;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
using HCE.Utility.Exceptions;
using HCE.Utility.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace HCE.Application.Features.LookupFeature.DataSourceFeature.Queries
{
    public class GetDataSourceDetailsQuery : IRequest<ResponseResult<DataSourceDto>>
    {
        public Guid Id { get; set; }
        private class Handler : IRequestHandler<GetDataSourceDetailsQuery, ResponseResult<DataSourceDto>>
        {
            private readonly IReadRepository<DataSource> _ReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<DataSource> ReadRepositor, IUserResolverHandler userResolverHandler)
            {
                _userResolverHandler = userResolverHandler;
                _ReadRepository = ReadRepositor;
            }

            public async Task<ResponseResult<DataSourceDto>> Handle(GetDataSourceDetailsQuery request, CancellationToken cancellationToken)
            {
                var dataSource = await _ReadRepository.GetAsync(x => x.Id == request.Id
                                                   );
                if (dataSource == null)
                    throw new EntityNotFoundException(Message_Resource.EntityNotFound);

                var result = new ResponseResult<DataSourceDto>
                {
                    Entity = new DataSourceDto()
                    {
                        DataSourceId = dataSource.Id,
                        DataSourceName = dataSource.DataSourceName,
                        DataSourceDesc = dataSource.DataSourceDesc,

                        CreationDate = dataSource.CreatedDate,
                        CreatedBy = dataSource.UserId,


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK
                };
                return result;

            }



            public class Validator : AbstractValidator<GetDataSourceDetailsQuery>
            {
                public Validator()
                {

                    RuleFor(x => x.Id).NotEmpty();




                }
            }


        }

    }
}