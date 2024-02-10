using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Entities.Lookup;
using HCE.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCE.Persistence.Configuration.TablesConfigrations.Lookup
{
    public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>

    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable(nameof(City), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.StateRegion).WithMany(x => x.Cities).HasForeignKey(x => x.StateRegionId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}