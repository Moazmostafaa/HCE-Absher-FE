using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.Common.Enum;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Enums;
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

namespace HCE.Application.Features.LookupFeature.WorldRegionFeature.Commands
{
   public  class AddWorldRegionCommand : CommandBase<ResponseResult<WorldRegionDto>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string NameLang { get; set; }
        public string Desc { get; set; }


        private class Handler : IRequestHandler<AddWorldRegionCommand, ResponseResult<WorldRegionDto>>
        {
            private readonly IWriteRepository<WorldRegion> _write;
            private readonly IReadRepository<WorldRegion> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<WorldRegion> read, IWriteRepository<WorldRegion> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository
       )
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
          }

            public async Task<ResponseResult<WorldRegionDto>> Handle(AddWorldRegionCommand request, CancellationToken cancellationToken)
            {
               
                var region = new WorldRegion
                {
                    WorldRegionNameAr = request.NameAr,
                    WorldRegionNameEn = request.NameEn,
                    WorldRegionNameLang = request.NameLang,
                    UserId = _userResolverHandler.GetUserGuid(),
                    WorldRegionDesc = request.Desc
                };

                await _write.AddAsync(region);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<WorldRegionDto>()
                {

                    Entity = new WorldRegionDto()
                    {
                        RegionId = region.Id,
                        RegionNameAr = region.WorldRegionNameAr,
                        RegionNameEn = region.WorldRegionNameEn,
                        RegionNameLang = region.WorldRegionNameLang,


                        CreationDate = region.CreatedDate,
                        CreatedBy = region.UserId,
                     

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddWorldRegionCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.NameAr).NotEmpty().WithMessage(Message_Resource.ArabicNameIsRequired);

                    RuleFor(x => x.NameEn).NotEmpty().WithMessage(Message_Resource.EnglishNameIsRequired);

                }
            }

        }
    }
}
