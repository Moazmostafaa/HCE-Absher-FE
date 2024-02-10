using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.Exceptions
{
    public class BusinessException : Exception
    {
        public List<string> Errors { get; set; } = new List<string>();
        public BusinessException(string message)
           : base(message)
        {
        }
        public BusinessException(string message, List<string> errors)
          : base(message)
        {
            Errors = errors;
        }

        public BusinessException(string message, Exception ex)
           : base(message, ex)
        {
        }

    }
}
