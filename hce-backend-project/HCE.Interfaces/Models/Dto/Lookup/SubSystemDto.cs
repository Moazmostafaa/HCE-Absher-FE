using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class SubSystemDto
    {
        public Guid SubSystemId { get; set; }
        public string SubSystemName { get; set; }
        public string SubSystemDesc { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
