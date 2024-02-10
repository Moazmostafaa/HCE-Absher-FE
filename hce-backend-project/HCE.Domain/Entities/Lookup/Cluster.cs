using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HCE.Domain.Entities.Lookup
{
    public class Cluster : UpdateSoftDeleteEntity<Guid>
    {
        public string ClusterNameAr { get; set; }

        public string ClusterNameEn { get; set; }
        public string ClusterNameLang { get; set; }
        public string ClusterDesc { get; set; }

        public Guid DistrictId { get; set; }
        public District District { get; set; }

        public string ClusterKml { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Site> Sites { get; set; }

    }
}