using HCE.Domain.Entities.Lookup;
using HCE.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HCE.Persistence.Configuration.TablesConfigrations.Lookup
{
    public class KPIFeedingLogsEntityTypeConfiguration : IEntityTypeConfiguration<KPIFeedingLog>

    {
        public void Configure(EntityTypeBuilder<KPIFeedingLog> builder)
        {
            builder.ToTable(nameof(KPIFeedingLog), TablesSchema.KPISchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.DataSource).WithMany(x => x.KPIFeedingLogs).HasForeignKey(x => x.DataSourceId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Customer).WithMany(x => x.KPIFeedingLogs).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Cell).WithMany(x => x.KPIFeedingLogs).HasForeignKey(x => x.CellId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
