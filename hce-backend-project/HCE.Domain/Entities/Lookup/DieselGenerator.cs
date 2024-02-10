using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Lookup
{
    public class DieselGenerator : UpdateSoftDeleteEntity<Guid>
    {
        public string DieselGeneratorName { get; set; }

        public string DieselGeneratorType { get; set; }
        public double DieselGeneratorCapacity { get; set; }
        public Guid CapacityUnitId { get; set; }
        public MeasuringUnit MeasuringUnitCapacity { get; set; }

        public double DieselGeneratorTankCapacity { get; set; }

        public Guid TankCapacityUnitId { get; set; }
        public MeasuringUnit MeasuringUnitTankCapacity { get; set; }

        public Guid SiteId { get; set; }
        public Site Site { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
