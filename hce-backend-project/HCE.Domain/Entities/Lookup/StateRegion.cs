using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Common.Enum;
using HCE.Domain.Entities.Identity;

namespace HCE.Domain.Entities.Lookup
{
    public class StateRegion : UpdateSoftDeleteEntity<Guid>
    {
        public string StateRegionNameAr { get; set; }

        public string StateRegionNameEn { get; set; }
        public string StateRegionNameLang { get; set; }
        public string StateRegionDesc { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }

        public string StateRegionKml { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<City> Cities { get; set; }


    }
}
