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
    public class DistrictEntityTypeConfiguration : IEntityTypeConfiguration<District>

    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable(nameof(District), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.City).WithMany(x => x.Districts).HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}