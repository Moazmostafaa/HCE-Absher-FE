using FluentValidation;
using HCE.Application.Common;
using HCE.Domain.Entities.KPIs;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Exceptions;
using HCE.Domain.ResponseModel;
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


namespace HCE.Application.Features.KPIFeature.Commands
{
    public class UpdateKpiCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid KpiId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public string DefinitionFormula { get; set; }
        public string Target { get; set; }
        public string Entity { get; set; }
        public string GoalOptimization { get; set; }
        public int BadThreshold { get; set; }
        public int GoodThreshold { get; set; }
        public bool IsExperienceCustomer { get; set; }
        public bool IsNps { get; set; }
        public string GenericFormula { get; set; }
        public string VendorFormula { get; set; }
        public int DefaultWeight { get; set; }
        public int CalculatedWeight { get; set; }
        public Guid CategoryId { get; set; }
        public Guid DomainId { get; set; }
        public Guid SubSystemId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid MeasuringUnitId { get; set; }
        public Guid CodecId { get; set; }
        public Guid ServiceId { get; set; }
        private class Handler : IRequestHandler<UpdateKpiCommand, ResponseResult<bool>>
        {
            private readonly IWriteRepository<Kpi> _write;
            private readonly IReadRepository<Kpi> _read;
            private readonly IReadRepository<Category> _readKpiCategory;
            private readonly IReadRepository<Domains> _readDomain;
            private readonly IReadRepository<SubSystem> _readSubSystem;
            private readonly IReadRepository<Codec> _readCodec;
            private readonly IReadRepository<Priority> _readPriority;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserResolverHandler _userResolverHandler;
            public Handler(IUserResolverHandler userResolverHandler, IReadRepository<Kpi> read,
                IWriteRepository<Kpi> write, IUnitOfWork unitOfWork,
                IReadRepository<Category> readKpiCategory, IReadRepository<Domains> readDomains,
                IReadRepository<SubSystem> readSubSystem, IReadRepository<Codec> readCodec,
                IReadRepository<Priority> readPriority)
            {
                _userResolverHandler = userResolverHandler;
                _write = write;
                _read = read;
                _unitOfWork = unitOfWork;
                _readKpiCategory = readKpiCategory;
                _readDomain = readDomains;
                _readSubSystem = readSubSystem;
                _readCodec = readCodec;
                _readPriority = readPriority;
            }
            public async Task<ResponseResult<bool>> Handle(UpdateKpiCommand request, CancellationToken cancellationToken)
            {

                var kpi = await _read.GetAsync(x => x.Id == request.KpiId);
                if (kpi == null)
                    throw new EntityNotFoundException(Message_Resource.NotFound);
                var kpiCategory = await _readKpiCategory.GetAsync(x => x.Id == request.CategoryId);
                if (kpiCategory == null)
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

                kpi.Name = request.Name;
                kpi.Description = request.Description;
                kpi.Abbreviation = request.Abbreviation;
                kpi.DefinitionFormula = request.DefinitionFormula;
                kpi.Target = request.Target;
                kpi.Entity = request.Entity;
                kpi.GoalOptimization = request.GoalOptimization;
                kpi.GenericFormula = request.GenericFormula;
                kpi.VendorFormula = request.VendorFormula;
                kpi.DefaultWeight = request.DefaultWeight;
                kpi.CalculatedWeight = request.CalculatedWeight;
                kpi.CodecId = request.CodecId;
                kpi.DomainId = request.DomainId;
                kpi.SubSystemId = request.SubSystemId;
                kpi.ServiceId = request.ServiceId;
                kpi.BadThreshold = request.BadThreshold;
                kpi.GoodThreshold = request.GoodThreshold;
                kpi.MeasuringUnitId = request.MeasuringUnitId;
                kpi.PriorityId = request.PriorityId;
                kpi.IsNps = request.IsNps;
                kpi.IsExperienceCustomer = request.IsExperienceCustomer;
                kpi.UpdatedBy = _userResolverHandler.GetUserId();
                kpi.UpdatedDate = DateTime.Now.GetCurrentDateTime();

                _write.Update(kpi);
                await _unitOfWork.CommitAsync();

                return new ResponseResult<bool>()
                {
                    Entity = true,
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = Message_Resource.DefaultSave
                };
            }
            public class Validator : AbstractValidator<UpdateKpiCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.KpiId).NotEmpty();
                    RuleFor(x => x.Name).NotEmpty();
                    RuleFor(x => x.Target).NotEmpty();
                    RuleFor(x => x.IsNps).NotEmpty();
                    RuleFor(x => x.IsExperienceCustomer).NotEmpty();
                }
            }

        }
    }
}