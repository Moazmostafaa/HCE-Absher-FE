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
    public class ClusterEntityTypeConfiguration : IEntityTypeConfiguration<Cluster>

    {
        public void Configure(EntityTypeBuilder<Cluster> builder)
        {
            builder.ToTable(nameof(Cluster), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.District).WithMany(x => x.Clusters).HasForeignKey(x => x.DistrictId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}