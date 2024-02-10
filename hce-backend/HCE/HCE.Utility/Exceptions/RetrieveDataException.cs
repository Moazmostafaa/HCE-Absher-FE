using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.Exceptions
{
    public class RetrieveDataException : Exception
    {
        public RetrieveDataException(string message)
           : base(message)
        {
        }

        public RetrieveDataException(string message, Exception ex)
           : base(message, ex)
        {
        }
    }
}
