using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class WorldRegionDto
    {
        public Guid RegionId { get; set; }
        public string RegionNameAr { get; set; }
        public string RegionNameEn { get; set; }
        public string RegionNameLang { get; set; }
        public string RegionDesc { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
