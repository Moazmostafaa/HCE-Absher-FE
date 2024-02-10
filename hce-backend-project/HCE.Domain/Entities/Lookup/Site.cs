using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HCE.Domain.Entities.Lookup
{
    public class Site : UpdateSoftDeleteEntity<Guid>
    {
        public string SiteName { get; set; }

        public string SiteDesc { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }


        public Guid ClusterId { get; set; }
        public Cluster Cluster { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<DieselGenerator> DieselGenerators { get; set; }
        public ICollection<Cell> Cells { get; set; }

        public ICollection<ExternalBaseStation> ExternalBaseStations { get; set; }


    }
}
