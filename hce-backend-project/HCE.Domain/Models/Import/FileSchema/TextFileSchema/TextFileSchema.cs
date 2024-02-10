using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Models.Import
{
    public class TextFileSchema
    {
        public string Code { get; set; }
        public string FileName { get; set; }
        public List<Records> Records { get; set; }
    }
}
