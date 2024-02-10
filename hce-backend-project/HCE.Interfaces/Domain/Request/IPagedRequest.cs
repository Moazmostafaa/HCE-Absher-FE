using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Domain.Request
{
    public interface IPagedRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
