using HCE.Interfaces.Enums;
using HCE.Interfaces.Models.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class StateRegionDto
    {
        public Guid RegionId { get; set; }
        public string RegionNameAr { get; set; }
        public string RegionNameEn { get; set; }
        public string RegionNameLang { get; set; }
        public string RegionDesc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CountryId { get; set; }
        public string CountryNameAr { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryNameLang { get; set; }
 
    }
}
