using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Domain.Response
{
    public interface IPagedResponseResult<TEntity>
    {
        int TotalRecords { get; set; }
        List<TEntity> Entities { get; set; }
    }
}
