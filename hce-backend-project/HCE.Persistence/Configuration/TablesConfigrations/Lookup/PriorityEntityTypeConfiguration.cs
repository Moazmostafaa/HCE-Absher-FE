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
    public class PriorityEntityTypeConfiguration : IEntityTypeConfiguration<Priority>

    {
        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.ToTable(nameof(Priority), TablesSchema.KPISchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);


        }
    }
}