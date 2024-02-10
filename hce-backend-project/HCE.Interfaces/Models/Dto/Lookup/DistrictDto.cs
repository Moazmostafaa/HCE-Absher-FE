using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class DistrictDto
    {
        public Guid DistrictId { get; set; }
        public string DistrictNameAr { get; set; }
        public string DistrictNameEn { get; set; }
        public string DistrictNameLang { get; set; }
        public string DistrictDesc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CityId { get; set; }
        public string CityNameAr { get; set; }
        public string CityNameEn { get; set; }
        public string CityNameLang { get; set; }
    }
}
