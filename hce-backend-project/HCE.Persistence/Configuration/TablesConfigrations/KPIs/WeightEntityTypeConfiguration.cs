using HCE.Domain.Entities.KPIs;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HCE.Persistence.Configuration.TablesConfigrations.KPIs
{
    public class WeightEntityTypeConfiguration : IEntityTypeConfiguration<Weight>

    {
        public void Configure(EntityTypeBuilder<Weight> builder)
        {
            builder.ToTable(nameof(Weight), TablesSchema.KPISchema);
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Service).WithOne(x => x.Weight).HasForeignKey<Weight>(x => x.ServiceId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Kpi).WithOne(x => x.Weight).HasForeignKey<Weight>(x => x.KpiId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

