using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.KPIs;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
using HCE.Interfaces.Repositories;
using HCE.Interfaces.UserResolverHandler;
using HCE.Resource;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HCE.Application.Features.KPIFeature.Commands
{
    public class AddKpiCommand : CommandBase<ResponseResult<bool>>
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public string DefinitionFormula { get; set; }
        public string Target { get; set; }
        public string Entity { get; set; }
        public string GoalOptimization { get; set; }
        public double BadThreshold { get; set; }
        public double GoodThreshold { get; set; }
        public bool IsExperienceCustomer { get; set; }
        public bool IsNps { get; set; }
        public string GenericFormula { get; set; }
        public string VendorFormula { get; set; }
        public double DefaultWeight { get; set; }
        public double CalculatedWeight { get; set; }
        public Guid CategoryId { get; set; }
        public Guid DomainId { get; set; }
        public Guid SubSystemId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid MeasuringUnitId { get; set; }
        public Guid CodecId { get; set; }
        public Guid ServiceId { get; set; }
        private class Handler : IRequestHandler<AddKpiCommand, ResponseResult<bool>>
        {
            private readonly IWriteRepository<Kpi> _write;
            private readonly IReadRepository<Category> _readKpiCategory;
            private readonly IReadRepository<Domains> _readDomain;
            private readonly IReadRepository<SubSystem> _readSubSystem;
            private readonly IReadRepository<Codec> _readCodec;
            private readonly IReadRepository<Priority> _readPriority;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            public Handler(IUserResolverHandler userResolverHandler, IWriteRepository<Kpi> write,
                IUnitOfWork unitOfWork, IReadRepository<Category> readKpiCategory,
                IReadRepository<Domains> readDomains, IReadRepository<SubSystem> readSubSystem,
                IReadRepository<Codec> readCodec, IReadRepository<Priority> readPriority)

            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _unitOfWork = unitOfWork;
                _readKpiCategory = readKpiCategory;
                _readDomain = readDomains;
                _readSubSystem = readSubSystem;
                _readCodec = readCodec;
                _readPriority = readPriority;
            }

            public async Task<ResponseResult<bool>> Handle(AddKpiCommand request, CancellationToken cancellationToken)
            {
                var category = await _readKpiCategory.GetAsync(x => x.Id == request.CategoryId);
                if (category == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var domains = await _readDomain.GetAsync(x => x.Id == request.DomainId);
                if (domains == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var codec = await _readCodec.GetAsync(x => x.Id == request.CodecId);
                if (codec == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var subSystem = await _readSubSystem.GetAsync(x => x.Id == request.SubSystemId);
                if (subSystem == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var service = await _readSubSystem.GetAsync(x => x.Id == request.ServiceId);
                if (service == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var priority = await _readPriority.GetAsync(x => x.Id == request.PriorityId);
                if (priority == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var measuringUnit = await _readSubSystem.GetAsync(x => x.Id == request.MeasuringUnitId);
                if (measuringUnit == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);

                var kpi = new Kpi
                {
                    Name = request.Name,
                    Description = request.Description,
                    Abbreviation = request.Abbreviation,
                    DefinitionFormula = request.DefinitionFormula,
                    Target = request.Target,
                    Entity = request.Entity,
                    GoalOptimization = request.GoalOptimization,
                    GenericFormula = request.GenericFormula,
                    VendorFormula = request.VendorFormula,
                    DefaultWeight = request.DefaultWeight,
                    CalculatedWeight = request.CalculatedWeight,
                    CodecId = request.CodecId,
                    DomainId = request.DomainId,
                    SubSystemId = request.SubSystemId,
                    ServiceId = request.ServiceId,
                    BadThreshold = request.BadThreshold,
                    GoodThreshold = request.GoodThreshold,
                    MeasuringUnitId = request.MeasuringUnitId,
                    PriorityId = request.PriorityId,
                    IsNps = request.IsNps,
                    IsExperienceCustomer = request.IsExperienceCustomer,
                    UserId = _userResolverHandler.GetUserGuid()
                };

                await _write.AddAsync(kpi);
                await _unitOfWork.CommitAsync();

                return new ResponseResult<bool>()
                {
                    Entity = true,
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };
            }
            public class Validator : AbstractValidator<AddKpiCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Name).NotEmpty();
                    RuleFor(x => x.Target).NotEmpty();
                    RuleFor(x => x.IsNps).NotEmpty();
                    RuleFor(x => x.IsExperienceCustomer).NotEmpty();
                }
            }

        }
    }
}