using HCE.Domain.Abstracts;
using HCE.Domain.Models.Import;
using HCE.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Customers
{
    public class MsOriginating : UpdateSoftDeleteEntity<Guid>
    {
        public string DateForStartOfCharge { get; set; }
        public string TimeForStartOfCharge { get; set; }
        public string TimeForStopOfCharge { get; set; }
        public string FirstCallingLocationInformation { get; set; }
        public string CallingPartyNumber { get; set; }
        public string EosInfo { get; set; }
        public string FirstAssignedSpeechCoderVersion { get; set; }
        public string InternalCauseAndLoc { get; set; }

        public static Expression<Func<MSOriginating, MsOriginating>> Projection
            => x => new()
            {
                CallingPartyNumber = x.CallingPartyNumber,
                TimeForStartOfCharge = x.TimeForStartOfCharge,
                TimeForStopOfCharge = x.TimeForStopOfCharge,
                FirstCallingLocationInformation = x.FirstCallingLocationInformation,
                DateForStartOfCharge = x.DateForStartOfCharge,
                FirstAssignedSpeechCoderVersion = x.FirstAssignedSpeechCoderVersion,
                InternalCauseAndLoc = x.InternalCauseAndLoc,
                EosInfo = x.EosInfo,
                CreatedDate = DateTime.Now.GetCurrentDateTime(),
                CreatedBy = "System",
            };
    }
}
