using HCE.Interfaces.Domain.Response;
using HCE.Utility.CommomEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Intergartions
{
    public interface IIntegrationService
    {
        Task<IResponseResult<TOutput>> GetHttpResponse<TOutput, TInput>(HttpVerb verb, string url, string endPoint, TInput input, bool throwException = false);
    }
}
