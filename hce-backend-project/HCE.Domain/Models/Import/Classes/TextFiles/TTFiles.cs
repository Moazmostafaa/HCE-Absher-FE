using HCE.Domain.Models.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Import
{
    public class TTFiles
    {
        public TTFiles()
        {
            MSOriginatings = new List<MSOriginating>();
        }
        public List<MSOriginating> MSOriginatings { get; set; }
    }
}
