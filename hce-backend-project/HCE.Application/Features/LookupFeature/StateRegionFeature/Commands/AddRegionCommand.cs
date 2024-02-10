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

namespace HCE.Application.Features.LookupFeature.StateRegionFeature.Commands
{
    public class AddRegionCommand : CommandBase<ResponseResult<StateRegionDto>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string NameLang { get; set; }
        public string Desc { get; set; }
        public Guid CountryId { get; set; }


        private class Handler : IRequestHandler<AddRegionCommand, ResponseResult<StateRegionDto>>
        {
            private readonly IWriteRepository<StateRegion> _write;
            private readonly IReadRepository<StateRegion> _read;

            private readonly IReadRepository<Country> _Countryread;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
          private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<StateRegion> read, IWriteRepository<StateRegion> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository,
          IReadRepository<Country> Countryread)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
                _Countryread = Countryread;
            }

            public async Task<ResponseResult<StateRegionDto>> Handle(AddRegionCommand request, CancellationToken cancellationToken)
            {
                var country = await _Countryread.GetAsync(x => x.Id == request.CountryId);
                if (country == null)
                    throw new EntityNotFoundException(Message_Resource.CountryEntity);

                var region = new StateRegion
                {
                    StateRegionNameAr = request.NameAr,
                    StateRegionNameEn = request.NameEn,
                    StateRegionNameLang = request.NameLang,
                    UserId = _userResolverHandler.GetUserGuid(),
                    StateRegionDesc = request.Desc,
                    CountryId=request.CountryId
                };

                await _write.AddAsync(region);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<StateRegionDto>()
                {

                    Entity = new StateRegionDto()
                    {
                        RegionId = region.Id,
                        RegionNameAr = region.StateRegionNameAr,
                        RegionNameEn = region.StateRegionNameEn,
                        RegionNameLang = region.StateRegionNameLang,
                   
                       
                        CreationDate = region.CreatedDate,
                        CreatedBy = region.UserId,
                        RegionDesc=region.StateRegionDesc,
                        CountryId = country.Id,
                        CountryNameAr = country.CountryNameAr,
                        CountryNameEn = country.CountryNameEn,
                        CountryNameLang = country.CountryNameLang,
                        
                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddRegionCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.CountryId).NotEmpty();

                    RuleFor(x => x.NameAr).NotEmpty().WithMessage(Message_Resource.ArabicNameIsRequired);

                    RuleFor(x => x.NameEn).NotEmpty().WithMessage(Message_Resource.EnglishNameIsRequired);


                }
            }

        }
    }
}

