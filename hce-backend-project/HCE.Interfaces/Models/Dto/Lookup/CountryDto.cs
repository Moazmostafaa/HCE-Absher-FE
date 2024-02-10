using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class CountryDto
    {
        public Guid CountryId { get; set; }
        public string CountryNameAr { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryNameLang { get; set; }
        public string CountryDesc { get; set; }

        public Guid WorldRegionId { get; set; }

        public string WorldRegionNameAr { get; set; }
        public string WorldRegionNameEn { get; set; }
        public string WorldRegionNameLang { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
    }

}
