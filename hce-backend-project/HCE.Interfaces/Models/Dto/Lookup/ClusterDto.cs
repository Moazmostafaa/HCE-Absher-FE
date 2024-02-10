using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.Lookup
{
    public class ClusterDto
    {
        public Guid ClusterId { get; set; }
        public string ClusterNameAr { get; set; }
        public string ClusterNameEn { get; set; }
        public string ClusterNameLang { get; set; }
        public string ClusterDesc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid DistrictId { get; set; }
        public string DistrictNameAr { get; set; }
        public string DistrictNameEn { get; set; }
        public string DistrictNameLang { get; set; }
    }
}
