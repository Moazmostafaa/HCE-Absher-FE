using HCE.Interfaces.Models.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Models.Import
{
    public class RawDataFromCRMComplains
    {
        public string MSISDN { get; set; }
        public string TicketID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public string ResolutionDate { get; set; }
        public string ResolvedBy { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string AssignedGroup { get; set; }
        public string CustomerPricePlan { get; set; }
        public string CustomerLanguage { get; set; }
        public string CustomerProfile { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}
