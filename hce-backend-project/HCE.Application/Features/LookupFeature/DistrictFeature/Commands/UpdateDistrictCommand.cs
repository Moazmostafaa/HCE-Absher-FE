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
using HCE.Utility.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.LookupFeature.DistrictFeature.Commands
{
    public class UpdateDistrictCommand : CommandBase<ResponseResult<DistrictDto>>
    {
        public Guid DistrictId { get; set; }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string NameLang { get; set; }
        public string Desc { get; set; }
        public Guid CityId { get; set; }


        private class Handler : IRequestHandler<UpdateDistrictCommand, ResponseResult<DistrictDto>>
        {
            private readonly IWriteRepository<District> _write;
            private readonly IReadRepository<District> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;
            private readonly IReadRepository<City> _Cityread;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<District> read, IWriteRepository<District> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository, IReadRepository<City> Cityread
          )
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
                _Cityread = Cityread;
            }

            public async Task<ResponseResult<DistrictDto>> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
            {
                var district = await _read.GetAsync(x => x.Id == request.DistrictId);
                if (district == null)
                    throw new EntityNotFoundException(Message_Resource.DistrictEntity);
                var city = await _Cityread.GetAsync(x => x.Id == request.CityId);
                if (city == null)
                    throw new EntityNotFoundException(Message_Resource.CityEntity);


                district.DistrictNameAr = request.NameAr;
                district.DistrictNameEn = request.NameEn;
                district.DistrictNameLang = request.NameLang;
                district.CityId = request.CityId;
                district.DistrictDesc = request.Desc;
                district.UpdatedBy = _userResolverHandler.GetUserId();
                district.UpdatedDate = DateTime.Now.GetCurrentDateTime();
                _write.Update(district);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<DistrictDto>()
                {

                    Entity = new DistrictDto()
                    {
                        DistrictId = district.Id,
                        DistrictNameAr = district.DistrictNameAr,
                        DistrictNameEn = district.DistrictNameEn,
                        DistrictNameLang = district.DistrictNameLang,
                        DistrictDesc = district.DistrictDesc,
                        CityId = city.Id,
                        CityNameAr = city.CityNameAr,
                        CityNameEn = city.CityNameEn,
                        CityNameLang = city.CityNameLang,


                        CreationDate = district.CreatedDate,
                        CreatedBy = district.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateDistrictCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.DistrictId).NotEmpty();

                    RuleFor(x => x.CityId).NotEmpty();

                    RuleFor(x => x.NameAr).NotEmpty().WithMessage(Message_Resource.ArabicNameIsRequired);

                    RuleFor(x => x.NameEn).NotEmpty().WithMessage(Message_Resource.EnglishNameIsRequired);

                }
            }

        }
    }
}
