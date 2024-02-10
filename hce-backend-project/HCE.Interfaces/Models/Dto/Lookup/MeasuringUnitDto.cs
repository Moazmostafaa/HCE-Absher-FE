using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class MeasuringUnitDto
    {
        public Guid MeasuringUnitId { get; set; }
        public string MeasuringUnitName { get; set; }

        public string MeasuringUnitDesc { get; set; }
             public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
