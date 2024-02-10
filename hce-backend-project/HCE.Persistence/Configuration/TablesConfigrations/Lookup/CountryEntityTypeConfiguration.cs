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
    public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>

    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable(nameof(Country), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.WorldRegion).WithMany(x => x.Countries).HasForeignKey(x => x.WordRegionId).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
