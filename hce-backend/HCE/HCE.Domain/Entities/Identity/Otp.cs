using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCE.Domain.Abstracts;

namespace HCE.Domain.Entities.Identity
{
    public class Otp : UpdateSoftDeleteEntity<Guid>
    {
        public Guid OtpId
        {
            get => Id;
            set => Id = value;
        }
        public string NationalId { get; set; }
        public string Code { get; set; }
        public int Tries { get; set; }
        public string TcnCode { get; set; }
        public bool IsUsed { get; set; }
        public bool IsExpired { get; set; }

    }
}
