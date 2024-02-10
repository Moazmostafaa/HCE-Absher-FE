using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class PriorityDto
    {
        public Guid PriorityId { get; set; }
        public string PriorityName { get; set; }
    public string PriorityDesc { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
