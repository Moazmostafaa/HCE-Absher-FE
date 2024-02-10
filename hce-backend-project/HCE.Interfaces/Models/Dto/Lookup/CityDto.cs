using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class CityDto
    {
        public Guid CityId { get; set; }
        public string CityNameAr { get; set; }
        public string CityNameEn { get; set; }
        public string CityNameLang { get; set; }
        public string CityDesc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid StateRegionId { get; set; }
        public string StateRegionNameAr { get; set; }
        public string StateRegionNameEn { get; set; }
        public string StateRegionNameLang { get; set; }
        public bool IsTop { get; set; }

    }
}
