using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Statistics
{
    public class OverallNpsDto
    {
        public decimal NpsValue { get; set; }
        public Guid CellId { get; set; }
        public string CellName { get; set; }
    }
}
