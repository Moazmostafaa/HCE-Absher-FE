using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class ExternalOperatorDto
    {
        public Guid ExternalOperatorId { get; set; }
        public string ExternalOperatorName { get; set; }
        public string ExternalOperatorDesc { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
