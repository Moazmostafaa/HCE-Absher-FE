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
    public class KpiCategoriesEntityTypeConfiguration : IEntityTypeConfiguration<KpiCategories>

    {
        public void Configure(EntityTypeBuilder<KpiCategories> builder)
        {
            builder.ToTable(nameof(KpiCategories), TablesSchema.KPISchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Category).WithMany(x => x.KpiCategories).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Kpi).WithMany(x => x.KpiCategories).HasForeignKey(x => x.KpiId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
