using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Models.Import
{
    public class Records
    {
        public string RecordName { get; set; }
        public string RecordClassName { get; set; }
        public List<RecordColumn> RecordColumns { get; set; }
    }
}
