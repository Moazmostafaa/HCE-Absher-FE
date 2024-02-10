using HCE.Domain.Abstracts;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Models.Import;
using HCE.Utility.Extensions;
using HCE.Utility.HelperOperation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace HCE.Domain.Entities.Customers
{
    public class CRMComplain : UpdateSoftDeleteEntity<Guid>
    {
        public string MSISDN { get; set; }
        public string TicketID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreationDate { get; set; }
        public string TicketCreatedBy { get; set; }
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


        public static Expression<Func<RawDataFromCRMComplains, CRMComplain>> Projection
            => x => new()
            {
                AssignedGroup = x.AssignedGroup,
                Country = x.Country,
                Category = x.Category,
                City = x.City,
                District = x.District,
                Province = x.Province,
                ResolutionDate = DateTimeHelper.UnifyDateFormat(x.ResolutionDate),
                ResolvedBy = x.ResolvedBy,
                Type = x.Type,
                CreationDate = DateTimeHelper.UnifyDateFormat(x.CreationDate),
                CustomerLanguage = x.CustomerLanguage,
                CustomerProfile = x.CustomerProfile,
                CustomerPricePlan = x.CustomerPricePlan,
                Description = x.Description,
                MSISDN = x.MSISDN,
                Status = x.Status,
                SubCategory = x.SubCategory,
                TicketCreatedBy = x.CreatedBy,
                TicketID = x.TicketID,
                CreatedDate = DateTime.Now.GetCurrentDateTime(),
                CreatedBy = "System",
            };

    }
}