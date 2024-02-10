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
    public class CellEntityTypeConfiguration : IEntityTypeConfiguration<Cell>

    {
        public void Configure(EntityTypeBuilder<Cell> builder)
        {
            builder.ToTable(nameof(Cell), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.AccessTechnology).WithMany(x => x.Cells).HasForeignKey(x => x.AccessTechnologyId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Vendor).WithMany(x => x.Cells).HasForeignKey(x => x.VendorId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Goal).WithMany(x => x.Cells).HasForeignKey(x => x.GoalId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Site).WithMany(x => x.Cells).HasForeignKey(x => x.SiteId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
