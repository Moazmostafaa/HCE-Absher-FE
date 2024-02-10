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
    public class DieselGeneratorEntityTypeConfiguration : IEntityTypeConfiguration<DieselGenerator>

    {
        public void Configure(EntityTypeBuilder<DieselGenerator> builder)
        {
            builder.ToTable(nameof(DieselGenerator), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.MeasuringUnitCapacity).WithMany(x => x.DieselGeneratorCapacities).HasForeignKey(x => x.CapacityUnitId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.MeasuringUnitTankCapacity).WithMany(x => x.DieselGeneratorTankCapacities).HasForeignKey(x => x.TankCapacityUnitId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Site).WithMany(x => x.DieselGenerators).HasForeignKey(x => x.SiteId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
