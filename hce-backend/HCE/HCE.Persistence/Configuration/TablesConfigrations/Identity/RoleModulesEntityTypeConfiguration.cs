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
    public class RoleModulesEntityTypeConfiguration : IEntityTypeConfiguration<RoleModules>
    {
        public void Configure(EntityTypeBuilder<RoleModules> builder)
        {
            builder.ToTable(nameof(RoleModules), TablesSchema.IdentitySchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Module).WithMany(x => x.RoleModules).HasForeignKey(x => x.ModuleId);
            builder.HasOne(x => x.Role).WithMany(x => x.RoleModules).HasForeignKey(x => x.RoleId);
        }
    }
}
