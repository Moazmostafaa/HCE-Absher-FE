using HCE.Domain.Entities.Customers;
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
    public class KPIEntityTypeConfiguration : IEntityTypeConfiguration<Kpi>

    {
        public void Configure(EntityTypeBuilder<Kpi> builder)
        {
            builder.ToTable(nameof(Kpi), TablesSchema.KPISchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Domain).WithMany(x => x.KPI).HasForeignKey(x => x.DomainId).OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.Codec).WithMany(x => x.KPI).HasForeignKey(x => x.CodecId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.SubSystem).WithMany(x => x.KPI).HasForeignKey(x => x.SubSystemId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Service).WithMany(x => x.Kpis).HasForeignKey(x => x.ServiceId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Priority).WithMany(x => x.KPI).HasForeignKey(x => x.PriorityId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.MeasuringUnit).WithMany(x => x.KPI).HasForeignKey(x => x.MeasuringUnitId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}