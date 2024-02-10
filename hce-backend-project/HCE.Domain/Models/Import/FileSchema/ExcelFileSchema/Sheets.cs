using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Models.Import
{
    public class Sheets
    {
        public string SheetName { get; set; }
        public string SheetClassName { get; set; }
        public List<SheetColumn> SheetColumns { get; set; }
    }
}
