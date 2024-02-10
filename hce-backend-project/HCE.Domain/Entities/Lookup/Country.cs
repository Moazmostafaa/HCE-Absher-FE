using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
namespace HCE.Domain.Entities.Lookup
{
    public class Country : UpdateSoftDeleteEntity<Guid>
    {
        public string CountryNameAr { get; set; }

        public string CountryNameEn { get; set; }
        public string CountryNameLang { get; set; }
        public string CountryDesc { get; set; }

        public Guid WordRegionId { get; set; }
        public WorldRegion WorldRegion { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<StateRegion> StateRegions { get; set; }
        public string CountryKml { get; set; }

        public ICollection<Operator> Operator { get; set; }
    }
}
