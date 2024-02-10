using HCE.Domain.Entities.Lookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Entities.TablesSchema;

namespace HCE.Persistence.Configuration.TablesConfigrations.Lookup
{
   public class RegionEntityTypeConfiguration : IEntityTypeConfiguration<StateRegion>

    {
        public void Configure(EntityTypeBuilder<StateRegion> builder)
        {
            builder.ToTable(nameof(StateRegion), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Country).WithMany(x => x.StateRegions).HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
