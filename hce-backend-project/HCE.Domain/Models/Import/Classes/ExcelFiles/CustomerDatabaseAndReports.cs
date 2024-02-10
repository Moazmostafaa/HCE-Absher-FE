using HCE.Domain.Models.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Import
{
    public class CustomerDatabaseAndReports
    {
        public CustomerDatabaseAndReports()
        {
            RawDataFromCRMComplains = new List<RawDataFromCRMComplains>();
        }
        public List<RawDataFromCRMComplains> RawDataFromCRMComplains { get; set; }
    }
}
