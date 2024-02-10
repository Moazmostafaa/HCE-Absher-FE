using HCE.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class ParentRegionDto
    {
        public Guid ParentRegionId { get; set; }
        public string ParentNameAr { get; set; }
        public string ParentNameEn { get; set; }
        public string ParentNameLang { get; set; }
        public string ParentDesc { get; set; }
        public RegionLevelEnum ParentLevel { get; set; }
        public bool IsParentTop { get; set; }

    }
}
