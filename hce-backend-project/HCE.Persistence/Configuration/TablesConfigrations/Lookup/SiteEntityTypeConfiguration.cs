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
    public class SiteEntityTypeConfiguration : IEntityTypeConfiguration<Site>

    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.ToTable(nameof(Site), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Cluster).WithMany(x => x.Sites).HasForeignKey(x => x.ClusterId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
