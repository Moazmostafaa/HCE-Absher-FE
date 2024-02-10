using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;

namespace HCE.Domain.Entities.Lookup
{
    public class City : UpdateSoftDeleteEntity<Guid>
    {
        public string CityNameAr { get; set; }

        public string CityNameEn { get; set; }
        public string CityNameLang { get; set; }
        public string CityDesc { get; set; }
        public bool IsTop { get; set; }
        public Guid StateRegionId { get; set; }
        public StateRegion StateRegion { get; set; }

        public string CityKml { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<District> Districts { get; set; }

    }
}
