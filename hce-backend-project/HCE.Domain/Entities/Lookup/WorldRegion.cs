using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;


namespace HCE.Domain.Entities.Lookup
{
    public class WorldRegion : UpdateSoftDeleteEntity<Guid>
    {
        public string WorldRegionNameAr { get; set; }

        public string WorldRegionNameEn { get; set; }
        public string WorldRegionNameLang { get; set; }
        public string WorldRegionDesc { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Country> Countries { get; set; }


    }
}
