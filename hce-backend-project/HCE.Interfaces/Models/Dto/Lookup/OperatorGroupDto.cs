using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class OperatorGroupDto
    {

        public Guid OperatorGroupId { get; set; }
        public string OperatorGroupName { get; set; }
        public string OperatorGroupDesc { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
