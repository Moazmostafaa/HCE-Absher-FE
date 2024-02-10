using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Interfaces.Models.Dto.Lookup;

namespace HCE.Interfaces.Models.Dto.Statistics
{
    public class TopCityNpsDto
    {
        public CityDto City { get; set; }
        public double CityNps { get; set; }
    }
}
