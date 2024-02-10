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
    public  class WorldRegionEntityTypeConfiguration : IEntityTypeConfiguration<WorldRegion>

    {
        public void Configure(EntityTypeBuilder<WorldRegion> builder)
        {
            builder.ToTable(nameof(WorldRegion), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
