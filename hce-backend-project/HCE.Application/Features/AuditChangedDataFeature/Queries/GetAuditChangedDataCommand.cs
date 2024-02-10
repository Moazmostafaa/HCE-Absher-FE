using HCE.Application.Common;
using HCE.Domain.Entities.Audit;
using HCE.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Application.Features.AuditChangedDataFeature.Queries
{
    public class GetAuditChangedDataCommand : QueryBase<ResponseResult<PagedResponseResult<AuditChangedData>>>
    {
        public GetAuditChangedDataCommand()
        { }

        public GetAuditChangedDataCommand(string tableName)
        {
            TableName = tableName;
        }

        public string TableName { get; set; }
    }
}
