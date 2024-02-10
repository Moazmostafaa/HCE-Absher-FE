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

namespace HCE.Application.Features.LookupFeature.CountryFeature.Commands
{
    public class AddCountryCommand : CommandBase<ResponseResult<CountryDto>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string NameLang { get; set; }
        public string Desc { get; set; }
        public Guid WorldRegionId { get; set; }


        private class Handler : IRequestHandler<AddCountryCommand, ResponseResult<CountryDto>>
        {
            private readonly IWriteRepository<Country> _write;
            private readonly IReadRepository<Country> _read;

            private readonly IReadRepository<WorldRegion> _WorldRegionRead;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Country> read, IWriteRepository<Country> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository,
          IReadRepository<WorldRegion> WorldRegionRead)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
                _WorldRegionRead = WorldRegionRead;
            }

            public async Task<ResponseResult<CountryDto>> Handle(AddCountryCommand request, CancellationToken cancellationToken)
            {
                var WorldRegion = await _WorldRegionRead.GetAsync(x => x.Id == request.WorldRegionId);
                if (WorldRegion == null)
                    throw new EntityNotFoundException(Message_Resource.WorldRegionEntity);

                var country = new Country
                {
                    CountryNameAr = request.NameAr,
                    CountryNameEn = request.NameEn,
                    CountryNameLang = request.NameLang,
                    UserId = _userResolverHandler.GetUserGuid(),
                    CountryDesc = request.Desc,
                    WordRegionId = request.WorldRegionId
                };

                await _write.AddAsync(country);

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
                        WorldRegionId = WorldRegion.Id,
                        WorldRegionNameAr = WorldRegion.WorldRegionNameAr,
                        WorldRegionNameEn = WorldRegion.WorldRegionNameEn,
                        WorldRegionNameLang = WorldRegion.WorldRegionNameLang

                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<AddCountryCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.WorldRegionId).NotEmpty();

                    RuleFor(x => x.NameAr).NotEmpty().WithMessage(Message_Resource.ArabicNameIsRequired);

                    RuleFor(x => x.NameEn).NotEmpty().WithMessage(Message_Resource.EnglishNameIsRequired);

                }
            }

        }
    }
}
