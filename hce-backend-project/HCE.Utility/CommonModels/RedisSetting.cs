using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.CommonModels
{
    public class RedisSetting
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public int DefaultSlidingExpirationInMinutes { get; set; }
        public int DefaultAbsoluteExpirationInMinutes { get; set; }
    }
}
