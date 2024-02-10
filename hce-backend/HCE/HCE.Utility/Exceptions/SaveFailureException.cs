using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.Exceptions
{
    public class SaveFailureException : Exception
    {
        public SaveFailureException(string message)
           : base(message)
        {
        }

        public SaveFailureException(string message, Exception ex)
           : base(message, ex)
        {
        }
    }
}
