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
    public class OperatorEntityTypeConfiguration : IEntityTypeConfiguration<Operator>

    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            builder.ToTable(nameof(Operator), TablesSchema.KPISchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.OperatorGroup).WithMany(x => x.Operators).HasForeignKey(x => x.OperatorGroupId).OnDelete(DeleteBehavior.NoAction);
    builder.HasOne(x => x.Country).WithMany(x => x.Operator).HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
