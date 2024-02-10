using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Models.Import
{
    public class SheetColumn
    {
        public string DataType { get; set; }
        public bool IsRequired { get; set; }
        public string ColumnName { get; set; }
        public string PropertyName { get; set; }
    }
}
