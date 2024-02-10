using HCE.Domain.Entities.Customers;
using HCE.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Persistence.Configuration.TablesConfigrations.Customers
{
    internal class MsOriginatingEntityTypeConfiguration : IEntityTypeConfiguration<MsOriginating>

    {
        public void Configure(EntityTypeBuilder<MsOriginating> builder)
        {
            builder.ToTable(nameof(MsOriginating), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);


        }
    }
}