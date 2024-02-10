using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class CodecDto
    {
        public Guid CodecId { get; set; }
        public string CodecName { get; set; }
        public string CodecDesc { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
