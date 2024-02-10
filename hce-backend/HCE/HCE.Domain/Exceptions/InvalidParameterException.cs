using HCE.Resource;
using HCE.Utility.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Exceptions
{
    public class InvalidParameterException : BusinessException
    {
        public InvalidParameterException(string parmeterName, string value)
            : base(string.Format(Message_Resource.InvalidParameter, value, parmeterName))
        {
        }
    }
}
