using HCE.Resource;
using HCE.Utility.Exceptions;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Exceptions
{
    public class EntityNotFoundException : BusinessException
    {
        public EntityNotFoundException(string entityName)
            : base(string.Format(Message_Resource.EntityNotFound, entityName))
        {
        }
    }
}
