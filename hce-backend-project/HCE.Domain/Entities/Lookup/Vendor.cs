using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HCE.Domain.Entities.Lookup

{
    public class Vendor : UpdateSoftDeleteEntity<Guid>
{
    public string VendorName { get; set; }

    public string VendorDesc { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
        public ICollection<Cell> Cells { get; set; }

    }
}