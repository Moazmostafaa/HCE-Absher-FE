using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Domain.Response
{
    public interface IResponse
    {
        string Message { get; set; }
        bool IsSuccess { get; set; }
        List<string> Errors { get; set; }
        HttpStatusCode Status { get; set; }
    }
}
