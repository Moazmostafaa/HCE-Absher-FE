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

namespace HCE.Application.Features.LookupFeature.CountryFeature.Commands
{
    public class UpdateCountryCommand : CommandBase<ResponseResult<CountryDto>>
    {
        public Guid CountryId { get; set; }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string NameLang { get; set; }
        public string Desc { get; set; }

        public Guid WorldRegionId { get; set; }


        private class Handler : IRequestHandler<UpdateCountryCommand, ResponseResult<CountryDto>>
        {
            private readonly IWriteRepository<Country> _write;
            private readonly IReadRepository<Country> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;
            private readonly IReadRepository<WorldRegion> _WorldRegionread;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Country> read, IWriteRepository<Country> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository, IReadRepository<WorldRegion> WorldRegionread)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
                _WorldRegionread = WorldRegionread;
            }

            public async Task<ResponseResult<CountryDto>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
            {
                var worldRegion = await _WorldRegionread.GetAsync(x => x.Id == request.WorldRegionId);
                if (worldRegion == null)
                    throw new EntityNotFoundException(Message_Resource.WorldRegionEntity);
                var country = await _read.GetAsync(x => x.Id == request.CountryId);
                if (country == null)
                    throw new EntityNotFoundException(Message_Resource.CountryEntity);


                country.CountryNameAr = request.NameAr;
                country.CountryNameEn = request.NameEn;
                country.CountryNameLang = request.NameLang;
                country.WordRegionId = request.WorldRegionId;
                country.CountryDesc = request.Desc;
                country.UpdatedBy = _userResolverHandler.GetUserId();
                country.UpdatedDate = DateTime.Now.GetCurrentDateTime();
                _write.Update(country);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<CountryDto>()
                {

                    Entity = new CountryDto()
                    {
                        CountryId = country.Id,
                        CountryNameAr = country.CountryNameAr,
                        CountryNameEn = country.CountryNameEn,
                        CountryNameLang = country.CountryNameLang,


                        CreationDate = country.CreatedDate,
                        CreatedBy = country.UserId,
                        CountryDesc = country.CountryDesc,
                        WorldRegionId = worldRegion.Id,
                        WorldRegionNameAr = worldRegion.WorldRegionNameAr,
                        WorldRegionNameEn = worldRegion.WorldRegionNameEn,
                        WorldRegionNameLang = worldRegion.WorldRegionNameLang

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateCountryCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.WorldRegionId).NotEmpty();

                    RuleFor(x => x.CountryId).NotEmpty();

                    RuleFor(x => x.NameAr).NotEmpty().WithMessage(Message_Resource.ArabicNameIsRequired);

                    RuleFor(x => x.NameEn).NotEmpty().WithMessage(Message_Resource.EnglishNameIsRequired);

                }
            }

        }
    }
}