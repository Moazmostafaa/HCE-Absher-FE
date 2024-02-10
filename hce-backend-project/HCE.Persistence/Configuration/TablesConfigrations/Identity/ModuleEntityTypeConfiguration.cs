using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Persistence.Configuration.TablesConfigrations.Identity
{
    public class ModuleEntityTypeConfiguration : IEntityTypeConfiguration<Modules>
    {
        public void Configure(EntityTypeBuilder<Modules> builder)
        {
            builder.ToTable(nameof(Modules), TablesSchema.IdentitySchema);
            builder.HasKey(x => x.ModuleId);
            builder.Property(x => x.ModuleId).ValueGeneratedNever();
        }
    }
}
