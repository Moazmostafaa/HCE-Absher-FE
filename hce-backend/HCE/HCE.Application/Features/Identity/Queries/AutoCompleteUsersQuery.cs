using HCE.Application.Common;
using HCE.Application.Common.Validators;
using HCE.Domain.Entities.Identity;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Domain.Request;
using HCE.Interfaces.Models.Dto.User;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Persistence.Repositories.Blob;
using HCE.Resource;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.Identity.Queries
{
    public class AutoCompleteUsersQuery : QueryBase<ResponseResult<PagedResponseResult<UserDto>>>, IPagedRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
        private class Handler : IRequestHandler<AutoCompleteUsersQuery, ResponseResult<PagedResponseResult<UserDto>>>
        {
            private readonly IReadRepository<User> _readRepo;
            private readonly IReadBlobRepository _blobRepo;
            private readonly IUserResolverHandler _userResolverHandler;

            public Handler(IReadRepository<User> readRepo, IReadBlobRepository blobRepo, IUserResolverHandler userResolverHandler)
            {
                _readRepo = readRepo;
                _blobRepo = blobRepo;
                _userResolverHandler = userResolverHandler;
            }

            public async Task<ResponseResult<PagedResponseResult<UserDto>>> Handle(AutoCompleteUsersQuery request, CancellationToken cancellationToken)
            {
                var expressions = new List<Expression<Func<User, bool>>>();
                expressions.Add(x => x.Id != _userResolverHandler.GetUserGuid());
                expressions.Add(x => string.IsNullOrEmpty(request.Query) || x.Name.ToLower().Contains(request.Query.ToLower()) || x.PhoneNumber.ToLower().Contains(request.Query.ToLower()));
                var predicate = _readRepo.CombineExpressions(expressions);

                var query = _readRepo.GetManyAsNoTracking(predicate, include: x => x.Include(c => c.UserRoles).ThenInclude(c => c.Role).Include(c => c.UserToken),
                                                                          orderBy: x => x.OrderByDescending(c => c.UserToken.LastLoginDate));
                var totalRecords = await query.CountAsync(cancellationToken: cancellationToken);
                query = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
                var result = new ResponseResult<PagedResponseResult<UserDto>>
                {
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Entity = new PagedResponseResult<UserDto>()
                    {
                        TotalRecords = totalRecords,
                        Entities = await query.Select(x => new UserDto()
                        {
                            UserId = x.Id,
                            Email = x.Email,
                            PhoneNumber = x.PhoneNumber,
                            UserName = x.UserName,
                            FullName = x.Name,
                            ProfileImageId = x.ProfileAttachmentId
                        }).ToListAsync(cancellationToken)
                    }
                };
                return result;
            }
        }

        public class Validator : AbstractValidator<AutoCompleteUsersQuery>
        {
            public Validator()
            {
                RuleFor(x => x).SetValidator(new PaginationValidator());
                RuleFor(x => x.Query).NotEmpty().WithMessage(Message_Resource.NameOrPhoneRequired);
            }
        }
    }
}
