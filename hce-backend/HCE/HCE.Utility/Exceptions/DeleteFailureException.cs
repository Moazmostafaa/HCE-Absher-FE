using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.Exceptions
{
    public class DeleteFailureException : Exception
    {
        public DeleteFailureException(string message)
           : base(message)
        {
        }

        public DeleteFailureException(string message, Exception ex)
           : base(message, ex)
        {
        }
    }
}
