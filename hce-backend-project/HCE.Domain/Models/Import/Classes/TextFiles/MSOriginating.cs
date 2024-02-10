using HCE.Interfaces.Models.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Models.Import
{
    public class MSOriginating
    {
        public string DateForStartOfCharge { get; set; }
        public string TimeForStartOfCharge { get; set; }
        public string TimeForStopOfCharge { get; set; }
        public string FirstCallingLocationInformation { get; set; }
        public string CallingPartyNumber { get; set; }
        public string EosInfo { get; set; }
        public string FirstAssignedSpeechCoderVersion { get; set; }
        public string InternalCauseAndLoc { get; set; }
    }
}
