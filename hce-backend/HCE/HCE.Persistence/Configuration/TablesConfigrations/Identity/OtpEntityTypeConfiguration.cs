using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCE.Persistence.Configuration.TablesConfigrations.Identity
{
    public class OtpEntityTypeConfiguration : IEntityTypeConfiguration<Otp>
    {
        public void Configure(EntityTypeBuilder<Otp> builder)
        {
            builder.ToTable(nameof(Otp), TablesSchema.IdentitySchema);
            builder.HasKey(x => x.Id);
            builder.HasIndex(e => e.NationalId)
                .IsUnique(false)
                .IsClustered(false);
        }
    }
}
