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
namespace HCE.Application.Features.LookupFeature.ClusterFeature.Commands
{
    public class UpdateClusterCommand : CommandBase<ResponseResult<ClusterDto>>
    {
        public Guid ClusterId { get; set; }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string NameLang { get; set; }
        public string Desc { get; set; }
        public Guid DistrictId { get; set; }


        private class Handler : IRequestHandler<UpdateClusterCommand, ResponseResult<ClusterDto>>
        {
            private readonly IWriteRepository<Cluster> _write;
            private readonly IReadRepository<Cluster> _read;

            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IReadRepository<User> _userReadRepository;
            private readonly IReadRepository<District> _Districtread;


            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Cluster> read, IWriteRepository<Cluster> write, IUnitOfWork unitOfWork,
          IReadRepository<User> userReadRepository, IReadRepository<District> Districtread)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _userReadRepository = userReadRepository;
                _Districtread = Districtread;
            }

            public async Task<ResponseResult<ClusterDto>> Handle(UpdateClusterCommand request, CancellationToken cancellationToken)
            {
                var district = await _Districtread.GetAsync(x => x.Id == request.DistrictId);
                if (district == null)
                    throw new EntityNotFoundException(Message_Resource.DistrictEntity);
                var cluster = await _read.GetAsync(x => x.Id == request.ClusterId);
                if (cluster == null)
                    throw new EntityNotFoundException(Message_Resource.ClusterEntity);


                cluster.ClusterNameAr = request.NameAr;
                cluster.ClusterNameEn = request.NameEn;
                cluster.ClusterNameLang = request.NameLang;
                cluster.DistrictId = request.DistrictId;
                cluster.ClusterDesc = request.Desc;
                cluster.UpdatedBy = _userResolverHandler.GetUserId();
                cluster.UpdatedDate = DateTime.Now.GetCurrentDateTime();
                _write.Update(cluster);

                await _unitOfWork.CommitAsync();


                return new ResponseResult<ClusterDto>()
                {

                    Entity = new ClusterDto()
                    {
                        DistrictId = district.Id,
                        DistrictNameAr = district.DistrictNameAr,
                        DistrictNameEn = district.DistrictNameEn,
                        DistrictNameLang = district.DistrictNameLang,
                        ClusterId = cluster.Id,
                        ClusterNameAr = cluster.ClusterNameAr,
                        ClusterNameEn = cluster.ClusterNameEn,
                        ClusterNameLang = cluster.ClusterNameLang,


                        CreationDate = cluster.CreatedDate,
                        CreatedBy = cluster.UserId


                    },
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };

            }


            public class Validator : AbstractValidator<UpdateClusterCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.DistrictId).NotEmpty();

                    RuleFor(x => x.ClusterId).NotEmpty();

                    RuleFor(x => x.NameAr).NotEmpty().WithMessage(Message_Resource.ArabicNameIsRequired);

                    RuleFor(x => x.NameEn).NotEmpty().WithMessage(Message_Resource.EnglishNameIsRequired);

                }
            }

        }
    }
}