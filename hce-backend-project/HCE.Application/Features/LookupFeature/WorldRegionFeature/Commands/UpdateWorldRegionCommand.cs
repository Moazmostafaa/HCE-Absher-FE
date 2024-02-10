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

namespace HCE.Application.Features.LookupFeature.WorldRegionFeature.Commands
{
    public class UpdateWorldRegionCommand : CommandBase<ResponseResult<WorldRegionDto>>
    {
        public Guid RegionId { get; set; }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string NameLang { get; set; }
        public string Desc { get; set; }
       


        private class Handler : IRequestHandler<UpdateWorldRegionCommand, ResponseResult<WorldRegionDto>>
        {
            private readonly IWriteRepository<WorldRegion> _write;
            private readonly IReadRepository<WorldRegion> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<WorldRegion> read, IWriteRepository<WorldRegion> write, IUnitOfWork unitOfWork)
            {
                _read = read;
                _write = write;
                _unitOfWork = unitOfWork;
                _userResolverHandler = userResolverHandler;
            }

            public async Task<ResponseResult<WorldRegionDto>> Handle(UpdateWorldRegionCommand request, CancellationToken cancellationToken)
            {
              
                var region = await _read.GetAsync(x => x.Id == request.RegionId);
    
                if (region == null)
                    throw new EntityNotFoundException(Message_Resource.WorldRegionEntity);

                region.WorldRegionNameAr = request.NameAr;
                region.WorldRegionNameEn = request.NameEn;
                region.WorldRegionNameLang = request.NameLang;
                region.WorldRegionDesc = request.Desc;
                region.UpdatedBy = _userResolverHandler.GetUserId();
                region.UpdatedDate = DateTime.Now.GetCurrentDateTime();
                _write.Update(region);

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
                        RegionDesc = region.WorldRegionDesc,
                    

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateWorldRegionCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.RegionId).NotEmpty();
                    RuleFor(x => x.NameAr).NotEmpty().WithMessage(Message_Resource.ArabicNameIsRequired);

                    RuleFor(x => x.NameEn).NotEmpty().WithMessage(Message_Resource.EnglishNameIsRequired);


                }
            }

        }
    }
}