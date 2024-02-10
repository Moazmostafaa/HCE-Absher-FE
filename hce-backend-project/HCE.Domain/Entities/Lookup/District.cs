using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;

namespace HCE.Domain.Entities.Lookup
{
    public class District : UpdateSoftDeleteEntity<Guid>
    {
        public string DistrictNameAr { get; set; }

        public string DistrictNameEn { get; set; }
        public string DistrictNameLang { get; set; }
        public string DistrictDesc { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }

        public string DistrictKml { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Cluster> Clusters { get; set; }

    }
}