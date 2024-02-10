using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class CoreTypeDto
    {
        public Guid NPSKPIWeightId { get; set; }
        public string NPSKPIWeightName { get; set; }

        public string NPSKPIWeightDesc { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
