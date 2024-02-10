using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HCE.Domain.Entities.KPIs
{
    public class Kpi : UpdateSoftDeleteEntity<Guid>
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
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid DomainId { get; set; }
        public Domains Domain { get; set; }
        public Guid SubSystemId { get; set; }
        public SubSystem SubSystem { get; set; }
        public Guid PriorityId { get; set; }
        public Priority Priority { get; set; }
        public Guid MeasuringUnitId { get; set; }
        public MeasuringUnit MeasuringUnit { get; set; }
        public Guid CodecId { get; set; }
        public Codec Codec { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
        public ICollection<KpiCategories> KpiCategories { get; set; }
        public Weight Weight { get; set; }
    }
}
