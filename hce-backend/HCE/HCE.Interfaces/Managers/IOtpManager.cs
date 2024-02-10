using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Managers
{
    public interface IOtpManager
    {
        Task<bool> SendOtp(string nationalId);
        Task<bool> ResendOtp(string nationalId);
        Task<bool> VerifyOtp(string nationalId, string code);
    }
}
