using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Entities.KPIs;
using HCE.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCE.Persistence.Configuration.TablesConfigrations.NPS
{
    internal class NpsEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.NPS.NpsResult>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.NPS.NpsResult> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Cell).WithMany().HasForeignKey(x => x.CellId);
        }
    }
}
