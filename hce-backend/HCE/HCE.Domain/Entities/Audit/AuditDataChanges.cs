using HCE.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Audit
{
    public class AuditChangedData : EntityBase<Guid>
    {
        public Guid AuditDataChangesId
        {
            get => Id;
            set => Id = value;
        }
        public string ChangeType { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string PrimaryKey { get; set; }
        public string ChangedColumns { get; set; }
        public Guid IdentifierSaveChangesId { get; set; }
    }
}
