using HCE.Domain.Entities.Audit;
using HCE.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Persistence.Configuration.TablesConfigrations.Audit
{
    public class AuditUserActionConfiguration : IEntityTypeConfiguration<AuditUserAction>
    {
        public void Configure(EntityTypeBuilder<AuditUserAction> builder)
        {
            builder.ToTable(nameof(AuditUserAction), TablesSchema.AuditSchema);
            builder.HasKey(x => x.AuditUserActionId);
            builder.Property(x => x.AuditUserActionId).ValueGeneratedOnAdd();
            builder.HasIndex(p => p.CreatedBy).HasDatabaseName("IX_CreatedBy").IsUnique(false);
            builder.HasIndex(p => p.CreatedDate).HasDatabaseName("IX_CreationDate").IsUnique(false);
        }
    }
}
