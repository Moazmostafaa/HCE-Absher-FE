using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.CommonModels
{
    public class AppOtpSettings
    {
        public string BaseUrl { get; set; }
        public string ProviderId { get; set; }
        public string SecretCode { get; set; }
        public int OtpMaxTriesNumber { get; set; }
        public int OtpValidForInMins { get; set; }
    }
}
