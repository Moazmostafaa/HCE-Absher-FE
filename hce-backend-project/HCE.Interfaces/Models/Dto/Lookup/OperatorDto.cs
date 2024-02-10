using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class OperatorDto
    {
        public Guid OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string OperatorDesc { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid OperatorGroupId { get; set; }
        public string OperatorGroupName { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
