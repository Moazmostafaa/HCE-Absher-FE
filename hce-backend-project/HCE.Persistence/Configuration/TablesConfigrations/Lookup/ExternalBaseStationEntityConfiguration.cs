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
    public class ExternalBaseStationEntityConfiguration : IEntityTypeConfiguration<ExternalBaseStation>

    {
        public void Configure(EntityTypeBuilder<ExternalBaseStation> builder)
        {
            builder.ToTable(nameof(ExternalBaseStation), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Site).WithMany(x => x.ExternalBaseStations).HasForeignKey(x => x.SiteId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
