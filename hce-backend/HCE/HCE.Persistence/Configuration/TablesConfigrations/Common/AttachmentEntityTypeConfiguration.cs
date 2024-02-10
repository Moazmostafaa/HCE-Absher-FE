using HCE.Domain.Entities.General;
using HCE.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Persistence.Configuration.TablesConfigrations.Chat
{
    public class AttachmentEntityTypeConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable(nameof(Attachment), TablesSchema.GeneralSchema);
            builder.HasKey(x => x.AttachmentId);
            builder.HasOne(x => x.Module).WithMany(x => x.Attachment).HasForeignKey(x => x.ModuleId);
        }
    }
}
