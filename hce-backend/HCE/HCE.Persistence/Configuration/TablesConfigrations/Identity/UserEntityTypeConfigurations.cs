using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HCE.Domain.Entities.Identity;
using HCE.Domain.Entities.TablesSchema;
using HCE.Domain.Entities.General;

namespace HCE.Persistence.Configuration.TablesConfigrations.Identity
{
    public class UserEntityTypeConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User), TablesSchema.IdentitySchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.UserToken).WithOne(x => x.User).HasForeignKey(typeof(UserToken), nameof(UserToken.UserId));

            builder.HasOne<Attachment>(ad => ad.ProfileAttachment).WithOne(s => s.UserProfile).HasForeignKey<User>(ad => ad.ProfileAttachmentId);
            builder.HasOne<Attachment>(ad => ad.IdentificationAttachment).WithOne(s => s.UserIdentification).HasForeignKey<User>(ad => ad.IdentificationAttachmentId);

            builder.HasIndex(e => e.UserName)
                .IsUnique()
                .IsClustered(false);

            builder.HasIndex(e => e.NationalId)
               .IsUnique()
               .IsClustered(false);


        }
    }
}
