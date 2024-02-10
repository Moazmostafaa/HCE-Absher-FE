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
    public class AuditChangedDataConfiguration : IEntityTypeConfiguration<AuditChangedData>
    {
        public void Configure(EntityTypeBuilder<AuditChangedData> builder)
        {
            builder.ToTable(nameof(AuditChangedData), TablesSchema.AuditSchema);
            builder.HasKey(x => x.AuditDataChangesId);
            builder.HasIndex(p => p.CreatedBy).HasDatabaseName("IX_CreatedBy").IsUnique(false);
            builder.HasIndex(p => p.TableName).HasDatabaseName("IX_TableName").IsUnique(false);
            builder.HasIndex(p => p.PrimaryKey).HasDatabaseName("IX_PrimaryKey").IsUnique(false);
            builder.HasIndex(p => p.SchemaName).HasDatabaseName("IX_SchemaName").IsUnique(false);
            builder.HasIndex(p => p.CreatedDate).HasDatabaseName("IX_CreationDate").IsUnique(false);
            builder.HasIndex(p => p.ChangeType).HasDatabaseName("IX_AuditDataChangeType").IsUnique(false);
        }
    }
}
