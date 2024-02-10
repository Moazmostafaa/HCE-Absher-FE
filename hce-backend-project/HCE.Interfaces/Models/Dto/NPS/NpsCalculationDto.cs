using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.NPS
{
    public class NpsCalculationDto
    {
        public decimal Nps { get; set; }
        public Guid CellId { get; set; }
    }
}
