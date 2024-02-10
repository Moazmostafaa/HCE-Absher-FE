using HCE.Domain.Entities.Customers;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HCE.Persistence.Configuration.TablesConfigrations.Customers
{
    public class CRMComplainEntityTypeConfiguration : IEntityTypeConfiguration<CRMComplain>

    {
        public void Configure(EntityTypeBuilder<CRMComplain> builder)
        {
            builder.ToTable(nameof(CRMComplain), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);


        }
    }
}