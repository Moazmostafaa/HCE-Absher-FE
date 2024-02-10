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

namespace HCE.Application.Features.LookupFeature.CityFeature.Commands
{
    public class AddCityFeatureCommand : CommandBase<ResponseResult<CityDto>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string NameLang { get; set; }
        public string Desc { get; set; }
        public Guid StateRegionId { get; set; }
        public bool IsTop { get; set; }

        private class Handler : IRequestHandler<AddCityFeatureCommand, ResponseResult<CityDto>>
        {
            private readonly IWriteRepository<City> _write;
            private readonly IReadRepository<City> _read;

            private readonly IReadRepository<StateRegion> _StateRegionread;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<City> read, IWriteRepository<City> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository,
          IReadRepository<StateRegion> StateRegionread)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
                _StateRegionread = StateRegionread;
            }

            public async Task<ResponseResult<CityDto>> Handle(AddCityFeatureCommand request, CancellationToken cancellationToken)
            {
                var stateRegion = await _StateRegionread.GetAsync(x => x.Id == request.StateRegionId);
                if (stateRegion == null)
                    throw new EntityNotFoundException(Message_Resource.StateRegionEntity);

                var city = new City
                {
                    CityNameAr = request.NameAr,
                    CityNameEn = request.NameEn,
                    CityNameLang = request.NameLang,
                    CityDesc=request.Desc,
                    UserId = _userResolverHandler.GetUserGuid(),
                    StateRegionId = request.StateRegionId,
                    IsTop=request.IsTop
                };

                await _write.AddAsync(city);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<CityDto>()
                {

                    Entity = new CityDto()
                    {
                        CityId = city.Id,
                        CityNameAr = city.CityNameAr,
                        CityNameEn = city.CityNameEn,
                        CityNameLang = city.CityNameLang,


                        CreationDate = city.CreatedDate,
                        CreatedBy = city.UserId,
                        CityDesc = city.CityDesc,
                        StateRegionId = stateRegion.Id,
                        StateRegionNameAr = stateRegion.StateRegionNameAr,
                        StateRegionNameEn = stateRegion.StateRegionNameEn,
                        StateRegionNameLang = stateRegion.StateRegionNameLang,
                        IsTop=city.IsTop

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddCityFeatureCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.StateRegionId).NotEmpty();

                    RuleFor(x => x.NameAr).NotEmpty().WithMessage(Message_Resource.ArabicNameIsRequired);

                    RuleFor(x => x.NameEn).NotEmpty().WithMessage(Message_Resource.EnglishNameIsRequired);

                }
            }

        }
    }
}

